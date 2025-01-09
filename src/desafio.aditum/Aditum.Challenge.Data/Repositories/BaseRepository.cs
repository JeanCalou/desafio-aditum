using System.Linq.Expressions;
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
        public async Task InsertMany(List<T> list)
        {
            await _collection.InsertManyAsync(list);
        }

        public async Task DeleteAllDocuments()
        {
            await _collection.DeleteManyAsync("{}");
        }

        public async Task AddOneAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
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
