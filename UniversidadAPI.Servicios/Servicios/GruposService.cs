using MongoDB.Driver;
using UniversidadAPI.Modelos;
using UniversidadAPI.UniversidadDatabaseSettings;

namespace UniversidadAPI.Servicios
{
    public class GruposService
    {
        private readonly IMongoCollection<Grupo> _gruposCollection;

        public GruposService(MongoDBInstance mongoDBInstance)
        {
            _gruposCollection = mongoDBInstance.GetDatabase().GetCollection<Grupo>("Grupo");
        }

        public async Task<List<Grupo>> GetAsync() =>
            await _gruposCollection.Find(_ => true).ToListAsync();

        public async Task<Grupo?> GetAsync(string id) =>
            await _gruposCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Grupo newGrupo) =>
            await _gruposCollection.InsertOneAsync(newGrupo);

        public async Task UpdateAsync(string id, Grupo updatedGrupo) =>
            await _gruposCollection.ReplaceOneAsync(x => x.Id == id, updatedGrupo);

        public async Task RemoveAsync(string id) =>
            await _gruposCollection.DeleteOneAsync(x => x.Id == id);
    }
}
