using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aditum.Challenge.Application.Models.Requests;
using Aditum.Challenge.Domain.Entities;

namespace Aditum.Challenge.Application.Mappers
{
    public static class RestaurantMapper
    {
        public static Restaurant ToRestaurantDomain(this RestaurantRequest restaurant)
        {
            return new Restaurant(new MongoDB.Bson.ObjectId(), restaurant.Name, restaurant.OpenHours);
        }
    }
}
