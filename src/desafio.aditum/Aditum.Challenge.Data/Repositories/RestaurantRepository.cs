using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
