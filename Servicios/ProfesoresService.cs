using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UniversidadAPI.Modelos;

namespace UniversidadAPI.Servicios
{
    public class ProfesoresService
    {
        private readonly IMongoCollection<Profesor> _profesoresCollection;

        public ProfesoresService(
                       IOptions<UniversidadDatabaseSettings> universidadDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                               universidadDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                               universidadDatabaseSettings.Value.DatabaseName);

            _profesoresCollection = mongoDatabase.GetCollection<Profesor>(
                               universidadDatabaseSettings.Value.ProfesoresCollectionName);
        }

        public async Task<List<Profesor>> GetAsync() =>
            await _profesoresCollection.Find(_ => true).ToListAsync();

        public async Task<Profesor?> GetAsync(string id) =>
            await _profesoresCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Profesor newProfesor) =>
            await _profesoresCollection.InsertOneAsync(newProfesor);

        public async Task UpdateAsync(string id, Profesor updatedProfesor) =>
            await _profesoresCollection.ReplaceOneAsync(x => x.Id == id, updatedProfesor);

        public async Task RemoveAsync(string id) =>
            await _profesoresCollection.DeleteOneAsync(x => x.Id == id);
    }
}
