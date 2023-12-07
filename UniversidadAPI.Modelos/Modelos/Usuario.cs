using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UniversidadAPI.Modelos;
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public int cedula { get; set; }
        public string clave { get; set; } = null!;
        public string tipo { get; set; } = null!;
    }

