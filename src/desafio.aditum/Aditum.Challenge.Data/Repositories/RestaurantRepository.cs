using Aditum.Challenge.Data.Repository;
using Aditum.Challenge.Domain.Entities;
using Aditum.Challenge.Domain.Interfaces;
using MongoDB.Driver;

namespace Aditum.Challenge.Data.Repositories
{
    public class RestaurantRepository(IMongoDatabase mongoDb) : BaseRepository<Restaurant>(mongoDb, "Restaurant"), IRestaurantRepository
    {
    }
}
