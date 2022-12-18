using TimeControl.Models;
using Microsoft.Extensions.Options;

namespace TimeControl.Services;

public class RoleService : BaseService<Role>
{
    public RoleService(IOptions<DatabaseSettings> databaseSettings) :
        base(databaseSettings, databaseSettings.Value.RoleCollectionName)
    { }
}