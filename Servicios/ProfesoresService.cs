using MongoDB.Driver;
using UniversidadAPI.Modelos;
using UniversidadAPI.UniversidadDatabaseSettings;

namespace UniversidadAPI.Servicios
{
    public class ProfesoresService
    {
        private readonly IMongoCollection<Profesor> _profesoresCollection;

        public ProfesoresService(MongoDBInstance mongoDBInstance)
        {
            _profesoresCollection = mongoDBInstance.GetDatabase().GetCollection<Profesor>("Profesor");
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
