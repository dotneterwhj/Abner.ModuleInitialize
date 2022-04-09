using Abner.ModuleInitialize;
using Microsoft.Extensions.DependencyInjection;
using Sample.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Repository
{
    internal class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITestRepository, TestRepository>();
        }
    }
}
