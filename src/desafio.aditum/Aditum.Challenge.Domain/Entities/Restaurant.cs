using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aditum.Challenge.Domain.Entities
{
    public class Restaurant(ObjectId _id, string name, string openHour, string closeHour)
    {
        [BsonId]
        public ObjectId _id { get; set; } = _id;
        public string Name { get; set; } = name;
        public string OpenHour { get; set; } = openHour;
        public string CloseHour { get; set; } = closeHour;
    }
}
