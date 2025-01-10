using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aditum.Challenge.Domain.Entities
{
    public class Restaurant(Guid id, string name, DateTime openHour, DateTime closeHour)
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public DateTime OpenHour { get; set; } = openHour;
        public DateTime CloseHour { get; set; } = closeHour;
    }
}
