using Microsoft.AspNetCore.Mvc;
using TimeControl.Models;
using TimeControl.Services;

namespace TimeControl.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnterpriseController : ControllerBase
{

    private readonly ILogger<EnterpriseController> _logger;
    private readonly EnterpriseService _enterpriseService;

    public EnterpriseController(ILogger<EnterpriseController> logger, EnterpriseService enterpriseService)
    {
        _logger = logger;
        _enterpriseService = enterpriseService;
    }

    [HttpGet]
    public async Task<List<Enterprise>> Get() =>
        await _enterpriseService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Enterprise>> Get(string id)
    {
        var enterprise = await _enterpriseService.GetAsync(id);

        if (enterprise is null)
        {
            return NotFound();
        }

        return enterprise;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Enterprise enterprise)
    {
        await _enterpriseService.CreateAsync(enterprise);

        return CreatedAtAction(nameof(Get), new { id = enterprise.Id }, enterprise);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Enterprise updatedEnterprise)
    {
        var enterprise = await _enterpriseService.GetAsync(id);

        if (enterprise is null)
        {
            return NotFound();
        }

        updatedEnterprise.Id = enterprise.Id;

        await _enterpriseService.UpdateAsync(id, updatedEnterprise);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _enterpriseService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _enterpriseService.RemoveAsync(id);

        return NoContent();
    }

}
