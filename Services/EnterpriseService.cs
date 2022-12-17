using TimeControl.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TimeControl.Services;

public class EnterpriseService
{
    private readonly IMongoCollection<Enterprise> _enterpriseCollection;

    public EnterpriseService(
        IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            databaseSettings.Value.DatabaseName);

        _enterpriseCollection = mongoDatabase.GetCollection<Enterprise>(
            databaseSettings.Value.EnterpriseCollectionName);
    }

    public async Task<List<Enterprise>> GetAsync() =>
        await _enterpriseCollection.Find(_ => true).ToListAsync();

    public async Task<Enterprise?> GetAsync(string id) =>
        await _enterpriseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Enterprise newBook) =>
        await _enterpriseCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, Enterprise updatedBook) =>
        await _enterpriseCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _enterpriseCollection.DeleteOneAsync(x => x.Id == id);
}