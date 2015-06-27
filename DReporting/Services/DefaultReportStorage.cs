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
        public static readonly string ReportsDir;
        public static readonly string CategoriesFile;

        const string categories_json = "categories.json";
        const string settings_json = "settings.json";
        const string xtrareport_xml = "xtrareport.xml";

        static DefaultReportStorage()
        {
            ReportsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Reports");
            CategoriesFile = Path.Combine(ReportsDir, categories_json);
            if (!Directory.Exists(ReportsDir)) { Directory.CreateDirectory(ReportsDir); }
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

        public IQueryable<ReportModel> QueryReports()
        {
            var dirs = Directory.GetDirectories(ReportsDir, "*", SearchOption.TopDirectoryOnly);

            var query = dirs.Select(x => this.GetReport(Path.GetFileName(x)));

            return query.AsQueryable();
        }

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

        public ReportModel GetDefaultReport()
        {
            var template = new DefaultXtraReport();

            return new ReportModel
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

        public ReportModel GetReport(string reportId)
        {
            if (string.IsNullOrEmpty(reportId))
            {
                return null;
            }

            var dir = new List<string> { Path.Combine(ReportsDir, reportId) };

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
            .Select(x => new ReportModel
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

        public CategoryModel GetCategory(string categoryId)
        {
            return this.QueryCategories().FirstOrDefault(x =>
                x.CategoryID.Equals(categoryId, StringComparison.InvariantCultureIgnoreCase));
        }

        public void DeleteReport(string reportId)
        {
            var dir = Path.Combine(ReportsDir, reportId);
            if (Directory.Exists(dir)) { Directory.Delete(dir, true); }
        }

        public void DeleteCategory(string categoryId)
        {
            var categories = this.QueryCategories();

            categories = categories.Where(x => x.CategoryID != categoryId);

            var dict = categories.ToDictionary(x => x.CategoryID, x => x.CategoryName);

            var json = JsonConvert.SerializeObject(dict, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(CategoriesFile, json);
        }

        public ReportModel SaveReport(ReportModel model)
        {
            var oldModel = this.GetReport(model.ReportID);

            if (string.IsNullOrEmpty(model.ReportID)) { model.ReportID = Guid.NewGuid().ToString().ToLower(); }

            var dir = Path.Combine(ReportsDir, oldModel != null ? oldModel.ReportID : model.ReportID);

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
    }
}
