using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using AdHocSensor.Application.Interfaces;
using AdHocSensor.Application.Services;

namespace AdHocSensor.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IPoiService, PoiService>();

            return services;
        }
    }
}