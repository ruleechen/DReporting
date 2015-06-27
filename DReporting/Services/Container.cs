using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBA.IoC;

namespace DReporting.Services
{
    public class Container
    {
        static Container _instance;
        static IContainer _container;
        static IEnumerable<ExportMeta> _exportMetas;

        static Container()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(i => i.FullName.StartsWith("DReporting", StringComparison.InvariantCultureIgnoreCase));
            _container = new ContainerConfiguration().WithAssemblies(assemblies).CreateContainer();

            _exportMetas = _container.Conventions.Exports.Select(x => new ExportMeta
            {
                ComponentType = x.ComponentType,
                ContractName = x.ContractName
            });

            _instance = new Container();
        }

        public class ExportMeta
        {
            public Type ComponentType { get; set; }
            public string ContractName { get; set; }
        }

        public static Container Instance
        {
            get
            {
                return _instance;
            }
        }

        public IEnumerable<ExportMeta> ExportMetas()
        {
            return _exportMetas;
        }

        public IEnumerable<T> ResolveValues<T>()
        {
            return _container.GetExports<T>();
        }

        public T ResolveValue<T>(string contractName = null)
        {
            if (string.IsNullOrEmpty(contractName))
            {
                return _container.GetExport<T>();
            }
            else
            {
                return _container.GetExport<T>(contractName);
            }
        }
    }
}
