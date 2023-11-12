using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UniversidadAPI.Modelos;

public class Curso
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string codigo { get; set; }
    public string nombre { get; set; }
    public int creditos { get; set; }
    public int horasSemanales { get; set; }
    public int ciclo { get; set; }
}