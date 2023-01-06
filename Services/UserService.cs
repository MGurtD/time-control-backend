using TimeControl.Models;
using TimeControl.Dtos;
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

    public async Task<LoginFormResult> CheckLoginForm(LoginForm loginForm) {

        var user = await GetByUsernameAsync(loginForm.Username);
        if (user == null)
        {
            return LoginFormResult.NotFound;
        }

        if (user.EnterpriseId != loginForm.Enterprise) 
        {
            return LoginFormResult.NotFoundInEnterpise;
        }

        if (user.Password != loginForm.Password) {
            return LoginFormResult.IncorrectPassword;
        }

        return LoginFormResult.Ok;
    }    

    public async Task<User?> GetByUsernameAsync(string username) {

        return await Collection.Find(x => x.Username == username).FirstOrDefaultAsync();
    }
}