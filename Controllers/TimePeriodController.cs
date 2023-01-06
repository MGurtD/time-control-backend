using Microsoft.AspNetCore.Mvc;
using TimeControl.Dtos;
using TimeControl.Models;
using TimeControl.Services;

namespace TimeControl.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimePeriodController : ControllerBase
{

    private readonly ILogger<TimePeriodController> _logger;
    private readonly TimePeriodService _timePeriodService;

    public TimePeriodController(ILogger<TimePeriodController> logger, TimePeriodService timePeriodService)
    {
        _logger = logger;
        _timePeriodService = timePeriodService;
    }

    [HttpGet]
    public async Task<List<TimePeriod>> Get() =>
        await _timePeriodService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<List<TimePeriodReadDto>>> GetByUserId(string id) =>
        await _timePeriodService.GetByUserIdAsync(id);

    [HttpPost]
    public async Task<IActionResult> Post(TimePeriod timePeriod)
    {
        await _timePeriodService.CreateAsync(timePeriod);

        return CreatedAtAction(nameof(Get), new { id = timePeriod.Id }, timePeriod);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, TimePeriod updatedTimePeriod)
    {
        var timePeriod = await _timePeriodService.GetAsync(id);
        if (timePeriod is null)
        {
            return NotFound();
        }

        updatedTimePeriod.Id = timePeriod.Id;

        await _timePeriodService.UpdateAsync(id, updatedTimePeriod);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var timePeriod = await _timePeriodService.GetAsync(id);
        if (timePeriod is null)
        {
            return NotFound();
        }

        await _timePeriodService.RemoveAsync(id);

        return NoContent();
    }

}
