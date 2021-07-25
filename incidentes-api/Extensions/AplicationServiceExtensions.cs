using incidentes_api.Interfaces;
using incidentes_api.Models;
using incidentes_api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace incidentes_api.Extensions
{
    public static class AplicationServiceExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<IncidentesDBContext>(options =>
                   options.UseSqlServer(config.GetConnectionString("defaultConnection")));
            return services;
        }
    }
}
