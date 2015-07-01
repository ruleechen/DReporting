using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DReporting.Models;
using DReporting.Web.Mvc.ViewModels;

namespace DReporting.Web.Mvc.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            var templates = this.ReportStorage.QueryTemplates();
            var categories = this.ReportStorage.QueryCategories();
            var providers = this.ReportStorage.QueryDataProviders();

            return View(new HomeVM
            {
                Templates = templates.Select(x => ToVM(x)),
                Categories = categories.Select(x => ToVM(x)),
                DataProviders = providers.Select(x => ToVM(x))
            });
        }

        private TemplateVM ToVM(TemplateModel model)
        {
            return new TemplateVM
            {
                TemplateID = model.TemplateID,
                TemplateCode = model.TemplateCode,
                TemplateName = model.TemplateName,
                CategoryID = model.CategoryID,
                CreationTime = model.CreationTime.ToLocalTime().ToString("yyyy-MM-dd hh:ss"),
                LastUpdateTime = model.LastUpdateTime.HasValue ? model.LastUpdateTime.Value.ToLocalTime().ToString("yyyy-MM-dd hh:ss") : string.Empty
            };
        }

        private CategoryVM ToVM(CategoryModel model)
        {
            return new CategoryVM
            {
                CategoryID = model.CategoryID,
                CategoryName = model.CategoryName
            };
        }

        private DataProviderVM ToVM(DataProviderModel model)
        {
            return new DataProviderVM
            {
                DataProviderID = model.DataProviderID,
                DataProviderName = model.Entity.DataProviderName,
                CategoryID = model.CategoryID
            };
        }

        public ActionResult EditTemplate(string templateId)
        {
            ViewData["Categories"] = this.ReportStorage.QueryCategories().Select(x => ToVM(x));

            var template = this.ReportStorage.GetTemplate(templateId);

            return View(ToVM(template));
        }

        [HttpPost]
        public ActionResult SaveTemplate(TemplateVM model, string returnUrl)
        {
            var template = this.ReportStorage.GetTemplate(model.TemplateID);

            template.TemplateCode = model.TemplateCode;
            template.TemplateName = model.TemplateName;
            template.CategoryID = model.CategoryID;
            template.LastUpdateTime = DateTime.UtcNow;

            this.ReportStorage.SaveTemplate(template);

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("EditTemplate", "Home", new { Area = ReportingContext.AreaName, TemplateID = model.TemplateID });
            }
        }

        public ActionResult CopyTemplate(string templateId)
        {
            var template = this.ReportStorage.GetTemplate(templateId);

            template.TemplateID = Guid.NewGuid().ToString().ToLower();
            template.TemplateName = "Copy of " + template.TemplateName;
            template.TemplateCode = "Copy of " + template.TemplateCode;
            template.CreationTime = DateTime.UtcNow;
            template.LastUpdateTime = null;

            this.ReportStorage.SaveTemplate(template);

            return RedirectToAction("Index", "Home", new { Area = ReportingContext.AreaName });
        }

        public ActionResult DeleteTemplate(string templateId)
        {
            this.ReportStorage.DeleteTemplate(templateId);
            return RedirectToAction("Index", "Home", new { Area = ReportingContext.AreaName });
        }

        public ActionResult CreateCategory()
        {
            return View(new CategoryVM());
        }

        public ActionResult EditCategory(string categoryId)
        {
            var category = this.ReportStorage.GetCategory(categoryId);
            return View(ToVM(category));
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
                return RedirectToAction("EditCategory", "Home", new { Area = ReportingContext.AreaName, CategoryID = model.CategoryID });
            }
        }

        public ActionResult DeleteCategory(string categoryId)
        {
            this.ReportStorage.DeleteCategory(categoryId);
            return RedirectToAction("Index", "Home", new { Area = ReportingContext.AreaName });
        }

        public ActionResult EditDataProvider(string dataProviderId)
        {
            ViewData["Categories"] = this.ReportStorage.QueryCategories().Select(x => ToVM(x));

            var provider = this.ReportStorage.GetDataProvider(dataProviderId);
            
            return View(ToVM(provider));
        }

        [HttpPost]
        public ActionResult SaveDataProvider(DataProviderVM model, string returnUrl)
        {
            this.ReportStorage.SaveDataProvider(new DataProviderModel
            {
                DataProviderID = model.DataProviderID,
                CategoryID = model.CategoryID
            });

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("EditDataProvider", "Home", new { Area = ReportingContext.AreaName, DataProviderID = model.DataProviderID });
            }
        }
    }
}
