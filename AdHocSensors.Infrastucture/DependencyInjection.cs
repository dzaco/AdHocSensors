using AdHocSensor.Application.Interfaces;
using AdHocSensors.Infrastucture.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Infrastucture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastucture(this IServiceCollection services)
        {
            services.AddScoped<IPoiRepository, PoiRepository>();
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddSingleton<ISettings, Settings>();

            return services;
        }
    }
}