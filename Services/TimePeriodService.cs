using TimeControl.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TimeControl.Services;

public class TimePeriodService : BaseService<TimePeriod>
{
    public TimePeriodService(IOptions<DatabaseSettings> databaseSettings) :
        base(databaseSettings, databaseSettings.Value.TimePeriodCollectionName)
    { 
        var indexKeysDefinition = Builders<TimePeriod>.IndexKeys.Ascending(user => user.UserId);
        
        Task task = base.CreateIndexAsync(indexKeysDefinition);
        task.Wait();
    }

    public async Task<List<TimePeriod>> GetByUserIdAsync(string userId) =>
        await base.Collection.Find(x => x.UserId == userId).ToListAsync(); 
    
}