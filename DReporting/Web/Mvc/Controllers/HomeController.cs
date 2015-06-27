using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DReporting.Models;

namespace DReporting.Web.Mvc.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            var reports = this.ReportStorage.QueryReports().Select(x => Convert(x));
            var categories = this.ReportStorage.QueryCategories().Select(x => Convert(x));
            var dataSources = this.ReportDatas.QueryDataSources().Select(x => Convert(x));

            return View(new HomeVM
            {
                Reports = reports,
                Categories = categories,
                DataSources = dataSources
            });
        }

        private ReportVM Convert(ReportModel model)
        {
            return new ReportVM
            {
                ReportID = model.ReportID,
                ReportCode = model.ReportCode,
                ReportName = model.ReportName,
                CategoryID = model.CategoryID,
                CreationTime = model.CreationTime.ToLocalTime().ToString("yyyy-MM-dd hh:ss"),
                LastUpdateTime = model.LastUpdateTime.HasValue ? model.LastUpdateTime.Value.ToLocalTime().ToString("yyyy-MM-dd hh:ss") : string.Empty
            };
        }

        private CategoryVM Convert(CategoryModel model)
        {
            return new CategoryVM
            {
                CategoryID = model.CategoryID,
                CategoryName = model.CategoryName
            };
        }

        private DataSourceVM Convert(DataSourceModel model)
        {
            return new DataSourceVM
            {
                DataSourceId = model.DataSourceId,
                DataSourceName = model.DataSourceName
            };
        }

        public ActionResult EditReport(string reportId)
        {
            ViewData["Categories"] = this.ReportStorage.QueryCategories().Select(x => Convert(x));

            var report = this.ReportStorage.GetReport(reportId);

            return View(Convert(report));
        }

        [HttpPost]
        public ActionResult SaveReport(ReportVM model, string returnUrl)
        {
            var report = this.ReportStorage.GetReport(model.ReportID);

            report.ReportCode = model.ReportCode;
            report.ReportName = model.ReportName;
            report.CategoryID = model.CategoryID;
            report.LastUpdateTime = DateTime.UtcNow;

            this.ReportStorage.SaveReport(report);

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("EditReport", "Home", new { Area = Consts.AreaName, ReportID = model.ReportID });
            }
        }

        public ActionResult CopyReport(string reportId)
        {
            var report = this.ReportStorage.GetReport(reportId);

            report.ReportID = Guid.NewGuid().ToString().ToLower();
            report.ReportName = "Copy of " + report.ReportName;
            report.ReportCode = "Copy of " + report.ReportCode;
            report.CreationTime = DateTime.UtcNow;
            report.LastUpdateTime = null;

            this.ReportStorage.SaveReport(report);

            return RedirectToAction("Index", "Home", new { Area = Consts.AreaName });
        }

        public ActionResult DeleteReport(string reportId)
        {
            this.ReportStorage.DeleteReport(reportId);
            return RedirectToAction("Index", "Home", new { Area = Consts.AreaName });
        }

        public ActionResult CreateCategory()
        {
            return View(new CategoryVM());
        }

        public ActionResult EditCategory(string categoryId)
        {
            var category = this.ReportStorage.GetCategory(categoryId);
            return View(Convert(category));
        }

        [HttpPost]
        public ActionResult SaveCategory(CategoryVM model, string returnUrl)
        {
            this.ReportStorage.SaveCategory(new CategoryModel
            {
                CategoryID = model.CategoryID,
                CategoryName = model.CategoryName
            });

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("EditCategory", "Home", new { Area = Consts.AreaName, CategoryID = model.CategoryID });
            }
        }

        public ActionResult DeleteCategory(string categoryId)
        {
            this.ReportStorage.DeleteCategory(categoryId);
            return RedirectToAction("Index", "Home", new { Area = Consts.AreaName });
        }
    }
}
