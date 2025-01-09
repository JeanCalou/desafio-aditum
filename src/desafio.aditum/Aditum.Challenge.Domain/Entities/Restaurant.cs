﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Aditum.Challenge.Domain.Entities
{
    public class Restaurant(Guid id, string name, string openHours)
    {
        [BsonId]
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string OpenHours { get; set; } = openHours;
    }
}
