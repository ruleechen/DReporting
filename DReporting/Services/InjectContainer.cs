using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using EBA.IoC;

namespace DReporting.Services
{
    public class InjectContainer
    {
        static object _registerLocker;
        static HashSet<Assembly> _assembilies;
        static InjectContainer _injectContainer;

        static IContainer _container;
        static IEnumerable<ExportMeta> _exportMetas;

        static InjectContainer()
        {
            _registerLocker = new object();
            _assembilies = new HashSet<Assembly>();
            _injectContainer = new InjectContainer();
        }

        static void Initialize()
        {
            _container = new ContainerConfiguration().WithAssemblies(_assembilies).CreateContainer();
            _exportMetas = _container.Conventions.Exports.Select(x => new ExportMeta
            {
                ComponentType = x.ComponentType,
                ContractName = x.ContractName
            });
        }

        public static void RegisterAssembiles(IEnumerable<Assembly> assembiles)
        {
            var count = _assembilies.Count;

            foreach (var item in assembiles)
            {
                if (!_assembilies.Contains(item))
                {
                    lock (_registerLocker)
                    {
                        if (!_assembilies.Contains(item))
                        {
                            _assembilies.Add(item);
                        }
                    }
                }
            }

            if (count != _assembilies.Count)
            {
                Initialize();
            }
        }

        public static InjectContainer Instance
        {
            get
            {
                return _injectContainer;
            }
        }

        #region Members

        public class ExportMeta
        {
            public Type ComponentType { get; set; }
            public string ContractName { get; set; }
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

        #endregion
    }
}
