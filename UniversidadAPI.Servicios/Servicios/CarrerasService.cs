using MongoDB.Driver;
using UniversidadAPI.Modelos;
using UniversidadAPI.UniversidadDatabaseSettings;

namespace UniversidadAPI.Servicios
{
    public class CarrerasService
    {
        private readonly IMongoCollection<Carrera> _carrerasCollection;

        public CarrerasService(MongoDBInstance mongoDBInstance)
        {
            _carrerasCollection = mongoDBInstance.GetDatabase().GetCollection<Carrera>("Carrera");

        }

        public async Task<List<Carrera>> GetAsync() =>
            await _carrerasCollection.Find(_ => true).ToListAsync();

        public async Task<Carrera?> GetAsync(string id) =>
            await _carrerasCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Carrera newCarrera) =>
            await _carrerasCollection.InsertOneAsync(newCarrera);

        public async Task UpdateAsync(string id, Carrera updatedCarrera) =>
            await _carrerasCollection.ReplaceOneAsync(x => x.Id == id, updatedCarrera);

        public async Task RemoveAsync(string id) =>
            await _carrerasCollection.DeleteOneAsync(x => x.Id == id);
    }
}
