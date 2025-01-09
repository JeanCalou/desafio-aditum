using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aditum.Challenge.Application.Interfaces;
using Aditum.Challenge.Application.Mappers;
using Aditum.Challenge.Application.Models.Requests;
using Aditum.Challenge.Application.Models.Responses;
using Aditum.Challenge.Domain.Entities;
using Aditum.Challenge.Domain.Interfaces;

namespace Aditum.Challenge.Application.Services
{
    public class RestaurantService(IRestaurantRepository restaurantRepository) : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
        public async Task AddAsync(RestaurantRequest restaurant)
        {
            var restaurantDomain = restaurant.ToRestaurantDomain();
            await _restaurantRepository.AddOneAsync(restaurantDomain);
        }

        public async Task DeleteAsync(RestaurantRequest restaurant)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Restaurant>> GetAllAsync()
        {
            return await _restaurantRepository.GetAllAsync();
        }

        public async Task UpdateAsync(RestaurantRequest restaurant)
        {
            throw new NotImplementedException();
        }
    }
}
