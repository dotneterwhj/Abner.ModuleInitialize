using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abner.ModuleInitialize
{
    public class AssemblyHelper
    {
        /// <summary>
        /// 获取所有引用的程序集
        /// </summary>
        /// <param name="assembly">最上端的程序集</param>
        /// <returns></returns>
        public static Assembly[]? GetAllRefrenceAssemblies(Assembly assembly, bool skipSystemAssembly = true)
        {
            // 如果选择跳过系统的程序集
            if (skipSystemAssembly && IsSystemAssembly(assembly))
            {
                return null;
            }

            var assemblies = new List<Assembly>();

            assemblies.Add(assembly);

            foreach (var assemblyName in assembly.GetReferencedAssemblies())
            {
                var refrenceAssembly = Assembly.Load(assemblyName);
                if (!assemblies.Contains(refrenceAssembly))
                {
                    if (skipSystemAssembly && IsSystemAssembly(refrenceAssembly))
                    {

                    }
                    else
                    {
                        assemblies.AddRange(GetAllRefrenceAssemblies(refrenceAssembly, skipSystemAssembly));
                    }
                }
            }

            return assemblies.Distinct().ToArray();
        }

        public static bool IsSystemAssembly(Assembly assembly)
        {
            var companyAttributes = assembly.GetCustomAttributes<AssemblyCompanyAttribute>();

            if (!companyAttributes.Any())
            {
                return false;
            }

            return companyAttributes.Where(s => s.Company.Contains("Microsoft")).Any();
        }

        /// <summary>
        /// 获取当前目录下的所有程序集 dll
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Assembly[]? GetAllAssemblies(string path, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var assemblies = new List<Assembly>();

            DirectoryInfo directory = new DirectoryInfo(path);

            if (directory.Exists)
            {
                var dlls = directory.GetFiles("*.dll", searchOption);
                foreach (var dll in dlls)
                {
                    var assembly = Assembly.LoadFile(dll.FullName);
                    assemblies.Add(assembly);
                }
            }
            return assemblies.Distinct().ToArray();
        }
    }
}
