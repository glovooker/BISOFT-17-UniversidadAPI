namespace UniversidadAPI.Modelos;

public class UniversidadDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string AlumnosCollectionName { get; set; } = null!;

    public string CarrerasCollectionName { get; set; } = null!;

    public string CiclosCollectionName { get; set; } = null!;

    public string CursosCollectionName { get; set; } = null!;

    public string GruposCollectionName { get; set; } = null!;

    public string ProfesoresCollectionName { get; set; } = null!;

    public string UsuariosCollectionName { get; set; } = null!;

}
