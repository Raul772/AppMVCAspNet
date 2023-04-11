using Microsoft.AspNetCore.Authorization;
using Teste.Extensions;

namespace Teste.Config
{
    public static class DependencyInjectionConfig
    {

        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();

            return services;
        }

    }
}
