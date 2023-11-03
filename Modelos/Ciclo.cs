using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UniversidadAPI.Modelos;

public class Ciclo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public int anio { get; set; }
    public int numero { get; set; }
    public DateTime fechaInicio { get; set; }
    public DateTime fechaFinalizacion { get; set; }
    public int id_ciclo { get; set; }
    public string estado { get; set; }
}