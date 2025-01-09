using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aditum.Challenge.Domain.Entities
{
    public class Restaurant(ObjectId _id, string name, string openHours)
    {
        [BsonId]
        public ObjectId _id { get; set; } = _id;
        public string Name { get; set; } = name;
        public string OpenHours { get; set; } = openHours;
    }
}
