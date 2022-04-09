using Microsoft.Extensions.DependencyInjection;

namespace Abner.ModuleInitialize
{
    public interface IModuleInitializer
    {
        void ConfigureServices(IServiceCollection services);
    }
}
