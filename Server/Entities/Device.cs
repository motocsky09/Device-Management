using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities
{
    public class Device
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Type { get; set; } // phone / tablet
        public string OperatingSystem { get; set; }
        public string OsVersion { get; set; }
        public string Processor { get; set; }
        public int Ram { get; set; }
        public string Description { get; set; }
        public string? AssignedUserId { get; set; } // null = nealocat
    }
}