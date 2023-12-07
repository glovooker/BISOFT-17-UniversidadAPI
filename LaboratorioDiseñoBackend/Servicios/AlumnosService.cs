using MongoDB.Driver;
using UniversidadAPI.Modelos;
using UniversidadAPI.UniversidadDatabaseSettings;

namespace UniversidadAPI.Servicios
{
    public class AlumnosService
    {
        private readonly IMongoCollection<Alumno> _alumnosCollection;

        public AlumnosService(MongoDBInstance mongoDBInstance)
        {
            _alumnosCollection = mongoDBInstance.GetDatabase().GetCollection<Alumno>("Alumno");

        }

        public async Task<List<Alumno>> GetAsync() =>
            await _alumnosCollection.Find(_ => true).ToListAsync();

        public async Task<Alumno?> GetAsync(string id) =>
            await _alumnosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Alumno newAlumno) =>
            await _alumnosCollection.InsertOneAsync(newAlumno);

        public async Task UpdateAsync(string id, Alumno updatedAlumno) =>
            await _alumnosCollection.ReplaceOneAsync(x => x.Id == id, updatedAlumno);

        public async Task RemoveAsync(string id) =>
            await _alumnosCollection.DeleteOneAsync(x => x.Id == id);
    }
}
