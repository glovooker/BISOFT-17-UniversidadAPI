using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UniversidadAPI.Modelos;

public class Profesor
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }

    public int cedula { get; set; }

    public string nombre { get; set; } = null!;

    public string telefono { get; set; } = null!;

    public string clave { get; set; } = null!;

    public string email { get; set; } = null!;

    public DateTime fechaNacimiento { get; set; }
}