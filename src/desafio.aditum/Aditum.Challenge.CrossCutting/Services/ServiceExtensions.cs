using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aditum.Challenge.Application.Interfaces;
using Aditum.Challenge.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Aditum.Challenge.CrossCutting.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServicos(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantService, RestaurantService>();
            return services;
        }
    }
}
