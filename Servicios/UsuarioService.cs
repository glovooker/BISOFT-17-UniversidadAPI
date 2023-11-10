using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UniversidadAPI.Modelos;

namespace UniversidadAPI.Servicios
{
    public class UsuariosService
    {
        private readonly IMongoCollection<Usuario> _usuariosCollection;

        public UsuariosService(
        IOptions<UniversidadDatabaseSettings> universidadDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                universidadDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                universidadDatabaseSettings.Value.DatabaseName);

            _usuariosCollection = mongoDatabase.GetCollection<Usuario>(
                universidadDatabaseSettings.Value.UsuariosCollectionName);
        }
        public async Task<List<Usuario>> GetAsync() =>
            await _usuariosCollection.Find(_ => true).ToListAsync();
        public async Task<Usuario?> GetAsync(string id) =>
            await _usuariosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Usuario newUsuario) => 
            await _usuariosCollection.InsertOneAsync(newUsuario);
        public async Task UpdateAsync(string id, Usuario updatedUsuario) => 
            await _usuariosCollection.ReplaceOneAsync(x => x.Id == id, updatedUsuario);
        public async Task RemoveAsync(string id) => 
            await _usuariosCollection.DeleteOneAsync(x => x.Id == id);
    }
}
