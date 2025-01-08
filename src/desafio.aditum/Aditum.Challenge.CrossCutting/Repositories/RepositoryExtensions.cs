using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aditum.Challenge.Data.Repositories;
using Aditum.Challenge.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Aditum.Challenge.CrossCutting.Repositories
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped< IRestaurantRepository, RestaurantRepository>();
            return services;
        }
    }
}
