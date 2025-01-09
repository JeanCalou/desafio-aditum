using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aditum.Challenge.Application.Models.Requests;
using Aditum.Challenge.Application.Models.Responses;

namespace Aditum.Challenge.Application.Interfaces
{
    public interface IRestaurantService
    {
        Task<RestaurantResponse> FindAsync();
        Task AddAsync(RestaurantRequest restaurant);
        Task UpdateAsync(RestaurantRequest restaurant);
        Task DeleteAsync(RestaurantRequest restaurant);
    }
}
