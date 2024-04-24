using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DotNet_With_MongoDB.Models
{
    public class AIModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string? ModelName { get; set; }

        public string? Specialization { get; set; }
    }
}
