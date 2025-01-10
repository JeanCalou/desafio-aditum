using Aditum.Challenge.Application.Interfaces;
using Aditum.Challenge.Domain.Entities;
using Aditum.Challenge.Domain.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Aditum.Challenge.Application.Services
{
    public class RestaurantService(IRestaurantRepository restaurantRepository) : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
        //public async Task AddAsync(RestaurantRequest restaurant)
        //{
        //    var restaurantDomain = restaurant.ToRestaurantDomain();
        //    await _restaurantRepository.AddOneAsync(restaurantDomain);
        //}

        public async Task InsertMany(List<Restaurant> restaurants)
        {
            await _restaurantRepository.InsertMany(restaurants);
        }

        public async Task<List<Restaurant>> GetAllByFilterAsync(DateTime time)
        {
            var filter = new FilterDefinitionBuilder<Restaurant>()
                .Where(x => x.OpenHour <= time && x.CloseHour >= time);

            return await _restaurantRepository.GetAllByFilterAsync(_ => filter.Inject());
        }

        public async Task DeleteAllDocuments()
        {
            await _restaurantRepository.DeleteAllDocuments();
        }
    }
}
