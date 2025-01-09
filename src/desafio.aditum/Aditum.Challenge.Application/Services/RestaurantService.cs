using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aditum.Challenge.Application.Interfaces;
using Aditum.Challenge.Application.Models.Requests;
using Aditum.Challenge.Application.Models.Responses;

namespace Aditum.Challenge.Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        public Task AddAsync(RestaurantRequest restaurant)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(RestaurantRequest restaurant)
        {
            throw new NotImplementedException();
        }

        public Task<RestaurantResponse> FindAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(RestaurantRequest restaurant)
        {
            throw new NotImplementedException();
        }
    }
}
