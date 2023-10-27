using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections;

namespace UniversidadAPI.Modelos;

public class Carrera
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string codigo { get; set; } = null!;

    public string nombre { get; set; } = null!;

    public string titulo { get; set; } = null!;

    public ArrayList cursos { get; set; } = null!;
}