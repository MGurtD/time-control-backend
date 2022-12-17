using TimeControl.Models;
using Microsoft.Extensions.Options;

namespace TimeControl.Services;

public class UserService : BaseService<Enterprise>
{
    public UserService(IOptions<DatabaseSettings> databaseSettings) :
        base(databaseSettings, databaseSettings.Value.EnterpriseCollectionName)
    { }
}