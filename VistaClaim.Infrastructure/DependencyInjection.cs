using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VistaClaim.Application.Common.Interfaces;
using VistaClaim.Infrastructure.Services;

namespace VistaClaim.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddVistaClaimInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
