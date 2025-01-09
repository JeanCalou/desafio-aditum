using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Aditum.Challenge.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Aditum.Challenge.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public BaseRepository(IMongoDatabase mongoDb, string collectionName)
        {
            MapClasses();
            _collection = mongoDb.GetCollection<T>(collectionName);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filterExpression)
        {
            var result = await _collection.Find(filterExpression).ToListAsync();

            return result;
        }

        public async Task<T> FindOneAsync(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var result = await _collection.Find(filter).FirstOrDefaultAsync();

            return result;
        }

        public async Task AddOneAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> filterExpression)
        {
            await _collection.DeleteOneAsync(filterExpression);
        }

        public async Task ReplaceOneAsync(Expression<Func<T, bool>> filterExpression, T entity, ReplaceOptions options)
        {
            await _collection.ReplaceOneAsync(filterExpression, entity, options);
        }

        private static void MapClasses()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
            {
                BsonClassMap.TryRegisterClassMap<T>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }        
    }
}
