using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aditum.Challenge.Application.Models.Responses
{
    public class RestaurantResponse(Guid id, string name, string openHours)
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string OpenHours { get; set; } = openHours;
    }
}
