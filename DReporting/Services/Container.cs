using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DReporting.Services
{
    public class Container
    {
        static Container _instance;
        static CompositionContainer _container;

        static Container()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(i => i.FullName.StartsWith("DReporting"));
            var catalog = new AggregateCatalog(assemblies.Select(x => new AssemblyCatalog(x)));
            _container = new CompositionContainer(catalog);
            _instance = new Container();
        }

        public static Container Instance
        {
            get
            {
                return _instance;
            }
        }

        public T ResolveValue<T>(string contractName = null)
        {
            if (string.IsNullOrEmpty(contractName))
            {
                return _container.GetExportedValue<T>();
            }
            else
            {
                return _container.GetExportedValue<T>(contractName);
            }
        }

        public IEnumerable<T> ResolveValues<T>()
        {
            return _container.GetExportedValues<T>();
        }

        //http://www.codewrecks.com/blog/index.php/2012/05/08/getting-the-list-of-type-associated-to-a-given-export-in-mef/
        //public IEnumerable<ReportInfo> AllReportInfos()
        //{
        //var fullName = typeof(ReportInfo).FullName;
        //return _container.Catalog.Parts.Where(p =>
        //    p.ExportDefinitions.Any(x =>
        //        x.Metadata.ContainsKey("ExportTypeIdentity") &&
        //        x.Metadata["ExportTypeIdentity"].Equals(fullName)))
        //    .Select(part => part.ExportDefinitions.FirstOrDefault().ContractName);
        //}
    }
}
