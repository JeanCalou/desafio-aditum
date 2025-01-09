using Aditum.Challenge.Application.Interfaces;
using Aditum.Challenge.Domain.Entities;
using Aditum.Challenge.Domain.Interfaces;

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

        public async Task<List<Restaurant>> GetAllAsync()
        {
            return await _restaurantRepository.GetAllAsync();
        }

        public async Task DeleteAllDocuments()
        {
            await _restaurantRepository.DeleteAllDocuments();
        }
    }
}
