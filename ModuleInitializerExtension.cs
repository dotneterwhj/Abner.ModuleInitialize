using Abner.ModuleInitialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ModuleInitializerExtension
    {
        public static IServiceCollection AddModuleInitializer(this IServiceCollection services)
        {
            services.AddModuleInitializer(AssemblyHelper.GetAllRefrenceAssemblies(Assembly.GetCallingAssembly()));

            return services;
        }

        private static IServiceCollection AddModuleInitializer(this IServiceCollection services, params Assembly[] assemblies)
        {
            // 如果没有指定的程序集则加载所有引用的程序集
            if (assemblies == null || assemblies.Length == 0)
            {
                var excuteAssm = new List<Assembly>();
                excuteAssm.AddRange(AssemblyHelper.GetAllRefrenceAssemblies(Assembly.GetEntryAssembly()));
                assemblies = excuteAssm.ToArray();
            }

            foreach (var assem in assemblies)
            {
                var moduleTypes = assem.GetTypes().Where(s => typeof(IModuleInitializer).IsAssignableFrom(s) && !s.IsAbstract);

                foreach (var moduleType in moduleTypes)
                {
                    var moduleInitializer = (IModuleInitializer?)Activator.CreateInstance(moduleType);

                    if (moduleInitializer == null)
                    {
                        throw new ApplicationException($"Cannot Create {moduleType.FullName} Type");
                    }

                    moduleInitializer.ConfigureServices(services);
                }
            }

            return services;
        }

        
    }
}
