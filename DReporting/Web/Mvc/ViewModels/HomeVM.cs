using System.Collections.Generic;

namespace DReporting.Web.Mvc.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<TemplateVM> Templates { get; set; }

        public IEnumerable<CategoryVM> Categories { get; set; }

        public IEnumerable<DataProviderVM> DataProviders { get; set; }
    }
}
