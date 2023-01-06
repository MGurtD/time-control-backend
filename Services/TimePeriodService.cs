using TimeControl.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TimeControl.Dtos;

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

    public async Task<List<TimePeriodReadDto>> GetByUserIdAsync(string userId) {
        List<TimePeriodReadDto> timePeriodDtos = new List<TimePeriodReadDto>();
        List<TimePeriod> timePeriods = await base.Collection.Find(x => x.UserId == userId).ToListAsync();

        foreach (var timePeriod in timePeriods)
        {
            timePeriodDtos.Add(new TimePeriodReadDto(timePeriod));
        }

        return timePeriodDtos;
    }
    
}