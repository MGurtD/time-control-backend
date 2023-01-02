using TimeControl.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TimeControl.Services;

public class BaseService<T> where T : BaseModel
{
    public readonly IMongoCollection<T> Collection;
    public readonly string CollectionName;

    public BaseService(IOptions<DatabaseSettings> databaseSettings, string collectionName)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        CollectionName = collectionName;
        Collection = mongoDatabase.GetCollection<T>(CollectionName);
    }

    public async Task CreateIndexAsync(IndexKeysDefinition<T> indexKeysDefinition)
    {
        await Collection.Indexes.CreateOneAsync(new CreateIndexModel<T>(indexKeysDefinition));
    }

    public async Task<List<T>> GetAsync() =>
        await Collection.Find(_ => true).ToListAsync();

    public async Task<T?> GetAsync(string id) =>
        await Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(T model)
    {
        model.CreatedOn = DateTime.Now;
        await Collection.InsertOneAsync(model);
    }

    public async Task UpdateAsync(string id, T model)
    {
        model.UpdatedAt = DateTime.Now;
        await Collection.ReplaceOneAsync(x => x.Id == id, model);
    }

    public async Task RemoveAsync(string id) =>
        await Collection.DeleteOneAsync(x => x.Id == id);
}