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
        public static readonly string CategoriesFile;

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

            CategoriesFile = Path.Combine(BaseDir, categories_json);
        }

        public class ReportSetting
        {
            public string ReportID { get; set; }
            public string ReportName { get; set; }
            public string ReportCode { get; set; }
            public string CategoryID { get; set; }
            public DateTime CreationTimeUtc { get; set; }
            public DateTime? LastUpdateTime { get; set; }
        }

        #endregion

        #region ITemplateMgr Members

        public IQueryable<TemplateModel> QueryReports()
        {
            var dirs = Directory.GetDirectories(TemplatesDir, "*", SearchOption.TopDirectoryOnly);

            var query = dirs.Select(x => this.GetReport(Path.GetFileName(x)));

            return query.AsQueryable();
        }

        public TemplateModel GetDefaultReport()
        {
            var template = new DefaultXtraReport();

            return new TemplateModel
            {
                ReportID = null,
                ReportCode = null,
                ReportName = null,
                CategoryID = null,
                CreationTime = DateTime.UtcNow,
                LastUpdateTime = null,
                XtraReport = template
            };
        }

        public TemplateModel GetReport(string reportId)
        {
            if (string.IsNullOrEmpty(reportId))
            {
                return null;
            }

            var dir = new List<string> { Path.Combine(TemplatesDir, reportId) };

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
                settings = JsonConvert.DeserializeObject<ReportSetting>(File.ReadAllText(x.settingsFile)),
                xtrareport = XtraReport.FromFile(x.xtrareportFile, true)
            })
            .Select(x => new TemplateModel
            {
                ReportID = x.settings.ReportID,
                ReportName = x.settings.ReportName,
                ReportCode = x.settings.ReportCode,
                CategoryID = x.settings.CategoryID,
                CreationTime = x.settings.CreationTimeUtc,
                LastUpdateTime = x.settings.LastUpdateTime,
                XtraReport = x.xtrareport
            });

            return query.FirstOrDefault();
        }

        public TemplateModel SaveReport(TemplateModel model)
        {
            var oldModel = this.GetReport(model.ReportID);

            if (string.IsNullOrEmpty(model.ReportID)) { model.ReportID = Guid.NewGuid().ToString().ToLower(); }

            var dir = Path.Combine(TemplatesDir, oldModel != null ? oldModel.ReportID : model.ReportID);

            if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }

            var settings = new ReportSetting
            {
                ReportID = oldModel != null ? oldModel.ReportID : model.ReportID,
                ReportCode = model.ReportCode,
                ReportName = model.ReportName,
                CategoryID = model.CategoryID,
                CreationTimeUtc = oldModel != null ? oldModel.CreationTime : DateTime.UtcNow,
                LastUpdateTime = oldModel != null ? DateTime.UtcNow : new Nullable<DateTime>()
            };

            var settingsPath = Path.Combine(dir, settings_json);
            File.WriteAllText(settingsPath, JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented));

            if (model.XtraReport != null)
            {
                model.XtraReport.Name = model.ReportID;
                model.XtraReport.DisplayName = model.ReportName;

                var reportPath = Path.Combine(dir, xtrareport_xml);
                File.WriteAllBytes(reportPath, model.XtraReport.GetBuffer());
            }

            return model;
        }

        public void DeleteReport(string reportId)
        {
            var dir = Path.Combine(TemplatesDir, reportId);
            if (Directory.Exists(dir)) { Directory.Delete(dir, true); }
        }

        #endregion

        #region ICategoryMgr Members

        public IQueryable<CategoryModel> QueryCategories()
        {
            if (!File.Exists(CategoriesFile))
            {
                return Enumerable.Empty<CategoryModel>().AsQueryable();
            }

            var json = File.ReadAllText(CategoriesFile);

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

            var category = categories.FirstOrDefault(x => x.CategoryID.Equals(model.CategoryID, StringComparison.InvariantCultureIgnoreCase));

            if (category == null)
            {
                if (string.IsNullOrEmpty(model.CategoryID))
                {
                    model.CategoryID = Guid.NewGuid().ToString().ToLower();
                }

                dict.Add(model.CategoryID, model.CategoryName);
            }
            else
            {
                dict[category.CategoryID] = model.CategoryName;
            }

            var json = JsonConvert.SerializeObject(dict, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(CategoriesFile, json);

            return model;
        }

        public void DeleteCategory(string categoryId)
        {
            var categories = this.QueryCategories();

            categories = categories.Where(x => x.CategoryID != categoryId);

            var dict = categories.ToDictionary(x => x.CategoryID, x => x.CategoryName);

            var json = JsonConvert.SerializeObject(dict, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(CategoriesFile, json);
        }

        #endregion

        #region IDataProviderMgr Members

        public IEnumerable<DataProviderModel> QueryDataProviders(int? skip = null, int? take = null)
        {
            var metas = InjectContainer.Instance.ExportMetas();

            var providers = InjectContainer.Instance.GetExports<IDataProvider>();

            var query = providers.Select(x => new DataProviderModel
            {
                DataProviderID = metas.First(m => m.ComponentType == x.GetType()).ContractName,
                Entity = x
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
            var provider = InjectContainer.Instance.GetExport<IDataProvider>(dataProviderId);

            return new DataProviderModel
            {
                DataProviderID = dataProviderId,
                Entity = provider
            };
        }

        public DataProviderModel SaveDataProvider(DataProviderModel model)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
