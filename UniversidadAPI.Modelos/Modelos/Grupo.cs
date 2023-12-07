using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections;

namespace UniversidadAPI.Modelos;

public class Grupo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public int ciclo { get; set; }

    public string curso { get; set; } = null!;

    public int numeroGrupo { get; set; }

    public string horario { get; set; } = null!;

    public int profesor { get; set; } 

    public List<int> estudiantes { get; set; } = null!;

    public List<List<int>> notas { get; set; } = null!;
}