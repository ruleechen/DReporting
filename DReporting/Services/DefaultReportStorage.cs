using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DevExpress.XtraReports.UI;
using DReporting.Core;
using DReporting.Models;

namespace DReporting.Services
{
    [Export(typeof(IReportStorage))]
    public class DefaultReportStorage : IReportStorage
    {
        #region Helpers

        public static readonly string BaseDir;
        public static readonly string ProvidersDir;
        public static readonly string TemplatesDir;

        const string categories_json = "categories.json";
        const string settings_json = "settings.json";
        const string xtrareport_xml = "xtrareport.xml";

        static DefaultReportStorage()
        {
            BaseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "DReporting");
            if (!Directory.Exists(BaseDir)) { Directory.CreateDirectory(BaseDir); }

            ProvidersDir = Path.Combine(BaseDir, "Providers");
            if (!Directory.Exists(ProvidersDir)) { Directory.CreateDirectory(ProvidersDir); }

            TemplatesDir = Path.Combine(BaseDir, "Templates");
            if (!Directory.Exists(TemplatesDir)) { Directory.CreateDirectory(TemplatesDir); }
        }

        public class TemplateSetting
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string CategoryID { get; set; }
            public DateTime CreationTimeUtc { get; set; }
            public DateTime? LastUpdateTime { get; set; }
        }

        #endregion

        #region ITemplateMgr Members

        public IQueryable<TemplateModel> QueryTemplates()
        {
            var dirs = Directory.GetDirectories(TemplatesDir, "*", SearchOption.TopDirectoryOnly);

            var query = dirs.Select(x => this.GetTemplate(Path.GetFileName(x))).Where(x => x != null);

            return query.AsQueryable();
        }

        public TemplateModel GetDefaultTemplate()
        {
            var template = new DefaultXtraReport();

            return new TemplateModel
            {
                TemplateID = null,
                TemplateCode = null,
                TemplateName = null,
                CategoryID = null,
                CreationTime = DateTime.UtcNow,
                LastUpdateTime = null,
                XtraReport = template
            };
        }

        public TemplateModel GetTemplate(string templateId)
        {
            if (string.IsNullOrEmpty(templateId))
            {
                return null;
            }

            var dir = new List<string> { Path.Combine(TemplatesDir, templateId) };

            var query = dir.Select(x => new
            {
                directory = x,
                settingsFile = Path.Combine(x, settings_json),
                xtrareportFile = Path.Combine(x, xtrareport_xml)
            })
            .Where(x =>
                Directory.Exists(x.directory) &&
                File.Exists(x.settingsFile) &&
                File.Exists(x.xtrareportFile)
            )
            .Select(x => new
            {
                settings = JsonConvert.DeserializeObject<TemplateSetting>(File.ReadAllText(x.settingsFile)),
                xtrareport = XtraReport.FromFile(x.xtrareportFile, true)
            })
            .Select(x => new TemplateModel
            {
                TemplateID = x.settings.ID,
                TemplateName = x.settings.Name,
                TemplateCode = x.settings.Code,
                CategoryID = x.settings.CategoryID,
                CreationTime = x.settings.CreationTimeUtc,
                LastUpdateTime = x.settings.LastUpdateTime,
                XtraReport = x.xtrareport
            });

            return query.FirstOrDefault();
        }

        public TemplateModel SaveTemplate(TemplateModel model)
        {
            var old = this.GetTemplate(model.TemplateID);

            if (string.IsNullOrEmpty(model.TemplateID)) { model.TemplateID = Guid.NewGuid().ToString().ToLower(); }

            var dir = Path.Combine(TemplatesDir, old != null ? old.TemplateID : model.TemplateID);

            if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }

            var settings = new TemplateSetting
            {
                ID = old != null ? old.TemplateID : model.TemplateID,
                Code = model.TemplateCode,
                Name = model.TemplateName,
                CategoryID = model.CategoryID,
                CreationTimeUtc = old != null ? old.CreationTime : DateTime.UtcNow,
                LastUpdateTime = old != null ? DateTime.UtcNow : new Nullable<DateTime>()
            };

            var settingsPath = Path.Combine(dir, settings_json);
            File.WriteAllText(settingsPath, JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented));

            if (model.XtraReport != null)
            {
                model.XtraReport.Name = model.TemplateID;
                model.XtraReport.DisplayName = model.TemplateName;

                var reportPath = Path.Combine(dir, xtrareport_xml);
                File.WriteAllBytes(reportPath, model.XtraReport.GetBuffer());
            }

            return model;
        }

        public void DeleteTemplate(string templateId)
        {
            var dir = Path.Combine(TemplatesDir, templateId);
            if (Directory.Exists(dir)) { Directory.Delete(dir, true); }
        }

        #endregion

        #region ICategoryMgr Members

        public IQueryable<CategoryModel> QueryCategories()
        {
            var categoriesFile = Path.Combine(BaseDir, categories_json);
            if (!File.Exists(categoriesFile))
            {
                return Enumerable.Empty<CategoryModel>().AsQueryable();
            }

            var json = File.ReadAllText(categoriesFile);

            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            var query = dict.Select(x => new CategoryModel
            {
                CategoryID = x.Key,
                CategoryName = x.Value
            });

            return query.AsQueryable();
        }

        public CategoryModel GetCategory(string categoryId)
        {
            return this.QueryCategories().FirstOrDefault(x =>
                x.CategoryID.Equals(categoryId, StringComparison.InvariantCultureIgnoreCase));
        }

        public CategoryModel SaveCategory(CategoryModel model)
        {
            var categories = this.QueryCategories();

            var dict = categories.ToDictionary(x => x.CategoryID, x => x.CategoryName);

            var item = categories.FirstOrDefault(x => x.CategoryID.Equals(model.CategoryID, StringComparison.InvariantCultureIgnoreCase));

            if (item == null)
            {
                if (string.IsNullOrEmpty(model.CategoryID))
                {
                    model.CategoryID = Guid.NewGuid().ToString().ToLower();
                }

                dict.Add(model.CategoryID, model.CategoryName);
            }
            else
            {
                dict[item.CategoryID] = model.CategoryName;
            }

            var json = JsonConvert.SerializeObject(dict, Newtonsoft.Json.Formatting.Indented);

            var categoriesFile = Path.Combine(BaseDir, categories_json);
            File.WriteAllText(categoriesFile, json);

            return model;
        }

        public void DeleteCategory(string categoryId)
        {
            var categories = this.QueryCategories();

            categories = categories.Where(x => x.CategoryID != categoryId);

            var dict = categories.ToDictionary(x => x.CategoryID, x => x.CategoryName);

            var json = JsonConvert.SerializeObject(dict, Newtonsoft.Json.Formatting.Indented);

            var categoriesFile = Path.Combine(BaseDir, categories_json);
            File.WriteAllText(categoriesFile, json);
        }

        #endregion

        #region IDataProviderMgr Members

        public IEnumerable<DataProviderModel> QueryDataProviders(int? skip = null, int? take = null)
        {
            var metas = InjectContainer.Instance.ExportMetas();
            var settings = this.QueryDataProviders().ToList();
            var providers = InjectContainer.Instance.GetExports<IDataProvider>();

            var query = providers.Select(x => new DataProviderModel
            {
                DataProviderID = metas.First(m => m.ComponentType == x.GetType()).ContractName,
                Entity = x
            });

            query = query.Select(item =>
            {
                item.CategoryID = settings.Where(x => x.DataProviderID.Equals(item.DataProviderID, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.CategoryID).FirstOrDefault();
                return item;
            });

            query = query.OrderBy(x => x.DataProviderID);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public DataProviderModel GetDataProvider(string dataProviderId)
        {
            var settings = this.QueryDataProviders().ToList();
            var provider = InjectContainer.Instance.GetExport<IDataProvider>(dataProviderId);

            return new DataProviderModel
            {
                DataProviderID = dataProviderId,
                CategoryID = settings.Where(x => x.DataProviderID.Equals(dataProviderId, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.CategoryID).FirstOrDefault(),
                Entity = provider
            };
        }

        public DataProviderModel SaveDataProvider(DataProviderModel model)
        {
            var providers = this.QueryDataProviders().ToList();

            var dict = providers.ToDictionary(x => x.DataProviderID, x => x.CategoryID);

            var item = providers.FirstOrDefault(x => x.DataProviderID.Equals(model.DataProviderID, StringComparison.InvariantCultureIgnoreCase));

            if (item == null)
            {
                dict.Add(model.DataProviderID, model.CategoryID);
            }
            else
            {
                dict[item.DataProviderID] = model.CategoryID;
            }

            var json = JsonConvert.SerializeObject(dict, Newtonsoft.Json.Formatting.Indented);

            var providersSettingFile = Path.Combine(ProvidersDir, settings_json);
            File.WriteAllText(providersSettingFile, json);

            return model;

        }

        private IQueryable<DataProviderModel> QueryDataProviders()
        {
            var providersSettingFile = Path.Combine(ProvidersDir, settings_json);
            if (!File.Exists(providersSettingFile))
            {
                return Enumerable.Empty<DataProviderModel>().AsQueryable();
            }

            var json = File.ReadAllText(providersSettingFile);

            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            var query = dict.Select(x => new DataProviderModel
            {
                DataProviderID = x.Key,
                CategoryID = x.Value
            });

            return query.AsQueryable();
        }

        #endregion
    }
}
