using TimeControl.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TimeControl.Services;

public class BaseService<T> where T : BaseModel
{
    private readonly IMongoCollection<T> _collection;
    private readonly string _collectionName;

    public BaseService(IOptions<DatabaseSettings> databaseSettings, string collectionName)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _collectionName = collectionName;
        _collection = mongoDatabase.GetCollection<T>(_collectionName);
    }

    public async Task<List<T>> GetAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<T?> GetAsync(string id) =>
        await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(T model)
    {
        model.CreatedOn = DateTime.Now;
        await _collection.InsertOneAsync(model);
    }

    public async Task UpdateAsync(string id, T model)
    {
        model.UpdatedAt = DateTime.Now;
        await _collection.ReplaceOneAsync(x => x.Id == id, model);
    }

    public async Task RemoveAsync(string id) =>
        await _collection.DeleteOneAsync(x => x.Id == id);
}