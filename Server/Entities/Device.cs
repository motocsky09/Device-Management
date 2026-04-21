using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities
{
    public class Device
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("manufacturer")]
        public string Manufacturer { get; set; }
        [BsonElement("type")]
        public string Type { get; set; } 
        [BsonElement("operatingSystem")]
        public string OperatingSystem { get; set; }
        [BsonElement("osVersion")]
        public string OsVersion { get; set; }
        [BsonElement("processor")]
        public string Processor { get; set; }
        [BsonElement("ram")]
        public int Ram { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("assignedUserId")]
        public string? AssignedUserId { get; set; }
    }
}