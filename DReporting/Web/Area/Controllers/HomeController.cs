using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DReporting.Models;

namespace DReporting.Web.Area.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            return View(new HomeVM
            {
                Reports = this.ReportService.AllReports(),
                DataSources = this.ReportService.AllDataSources()
            });
        }
    }
}
