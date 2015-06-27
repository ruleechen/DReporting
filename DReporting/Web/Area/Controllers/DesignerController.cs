using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DReporting.Models;
using DReporting.Services;

namespace DReporting.Web.Area.Controllers
{
    public class DesignerController : ControllerBase
    {
        public ActionResult Index(string reportId)
        {
            return View(new DesignerVM
            {
                Report = this.Service.GetReport(reportId)
            });
        }
    }
}
