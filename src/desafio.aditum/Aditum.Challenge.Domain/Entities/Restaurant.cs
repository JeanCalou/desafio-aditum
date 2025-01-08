using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aditum.Challenge.Domain.Entities
{
    public class Restaurant(string name, string openHours)
    {
        public string Name { get; set; } = name;
        public string OpenHours { get; set; } = openHours;
    }
}
