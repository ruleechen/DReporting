using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBA.IoC;

namespace DReporting.Services
{
    public class InjectContainer
    {
        static IContainer _container;
        static IEnumerable<ExportMeta> _exportMetas;
        static InjectContainer _injectContainer;

        static InjectContainer()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(i => i.FullName.StartsWith("DReporting", StringComparison.InvariantCultureIgnoreCase));
            _container = new ContainerConfiguration().WithAssemblies(assemblies).CreateContainer();

            _exportMetas = _container.Conventions.Exports.Select(x => new ExportMeta
            {
                ComponentType = x.ComponentType,
                ContractName = x.ContractName
            });

            _injectContainer = new InjectContainer();
        }

        public class ExportMeta
        {
            public Type ComponentType { get; set; }
            public string ContractName { get; set; }
        }

        public static InjectContainer Instance
        {
            get
            {
                return _injectContainer;
            }
        }

        public IEnumerable<ExportMeta> ExportMetas()
        {
            return _exportMetas;
        }

        public IEnumerable<T> GetExports<T>()
        {
            return _container.GetExports<T>();
        }

        public T GetExport<T>(string contractName = null)
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
