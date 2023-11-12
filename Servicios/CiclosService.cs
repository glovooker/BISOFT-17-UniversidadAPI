using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UniversidadAPI.Modelos;

namespace UniversidadAPI.Servicios
{
    public class CiclosService
    {
        private readonly IMongoCollection<Ciclo> _ciclosCollection;

        public CiclosService(
        IOptions<UniversidadDatabaseSettings> universidadDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                universidadDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                universidadDatabaseSettings.Value.DatabaseName);

            _ciclosCollection = mongoDatabase.GetCollection<Ciclo>(
                universidadDatabaseSettings.Value.CiclosCollectionName);
        }

        public async Task<List<Ciclo>> GetAsync() =>
            await _ciclosCollection.Find(_ => true).ToListAsync();

        public async Task<Ciclo?> GetAsync(string id) =>
            await _ciclosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Ciclo newCiclo) =>
            await _ciclosCollection.InsertOneAsync(newCiclo);

        public async Task UpdateAsync(string id, Ciclo updatedCiclo) =>
            await _ciclosCollection.ReplaceOneAsync(x => x.Id == id, updatedCiclo);

        public async Task RemoveAsync(string id) =>
            await _ciclosCollection.DeleteOneAsync(x => x.Id == id);
    }
}