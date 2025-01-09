using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aditum.Challenge.CrossCutting.Model;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Aditum.Challenge.CrossCutting.Mongo
{
    public static class MongoExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, MongoSettings mongoSettings)
        {
            var clientSettings = MongoClientSettings.FromConnectionString(mongoSettings.ConnectionString);
            var mongoClient = new MongoClient(clientSettings);
            services.AddSingleton<IMongoClient>(_ => mongoClient);

            services.AddSingleton(sp =>
            {
                var mongoClient = sp.GetService<IMongoClient>();
                var db = mongoClient!.GetDatabase(mongoSettings.DataBaseName);
                return db;
            });

            return services;
        }
    }
}
