using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UniversidadAPI.Modelos;

namespace UniversidadAPI.Servicios
{
    public class CursosService
    {
        private readonly IMongoCollection<Curso> _cursosCollection;

        public CursosService(
        IOptions<UniversidadDatabaseSettings> universidadDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                universidadDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                universidadDatabaseSettings.Value.DatabaseName);

            _cursosCollection = mongoDatabase.GetCollection<Curso>(
                universidadDatabaseSettings.Value.CursosCollectionName);
        }

        public async Task<List<Curso>> GetAsync() =>
            await _cursosCollection.Find(_ => true).ToListAsync();

        public async Task<Curso?> GetAsync(string id) =>
            await _cursosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Curso newCurso) =>
            await _cursosCollection.InsertOneAsync(newCurso);

        public async Task UpdateAsync(string id, Curso updatedCurso) =>
            await _cursosCollection.ReplaceOneAsync(x => x.Id == id, updatedCurso);

        public async Task RemoveAsync(string id) =>
            await _cursosCollection.DeleteOneAsync(x => x.Id == id);
    }
}
