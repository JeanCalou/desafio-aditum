using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aditum.Challenge.Domain.Entities
{
    public class Restaurant(ObjectId _id, string name, DateTime openHour, DateTime closeHour)
    {
        [BsonId]
        public ObjectId _id { get; set; } = _id;
        public string Name { get; set; } = name;
        public DateTime OpenHour { get; set; } = openHour;
        public DateTime CloseHour { get; set; } = closeHour;
    }
}
