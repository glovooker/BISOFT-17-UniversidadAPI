﻿namespace UniversidadAPI.Modelos;

public class UniversidadDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string AlumnosCollectionName { get; set; } = null!;

    public string CarrerasCollectionName { get; set; } = null!;
}
