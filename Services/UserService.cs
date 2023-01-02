using TimeControl.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TimeControl.Services;

public class UserService : BaseService<User>
{
    public UserService(IOptions<DatabaseSettings> databaseSettings) :
        base(databaseSettings, databaseSettings.Value.UserCollectionName)
    { 
        var indexKeysDefinition = Builders<User>.IndexKeys.Ascending(user => user.EnterpriseId);
        
        Task task = base.CreateIndexAsync(indexKeysDefinition);
        task.Wait();
    }
    
}