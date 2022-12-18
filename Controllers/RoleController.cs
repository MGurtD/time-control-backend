using Microsoft.AspNetCore.Mvc;
using TimeControl.Models;
using TimeControl.Services;

namespace TimeControl.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{

    private readonly ILogger<RoleController> _logger;
    private readonly RoleService _roleService;

    public RoleController(ILogger<RoleController> logger, RoleService roleService)
    {
        _logger = logger;
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<List<Role>> Get() =>
        await _roleService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Role>> Get(string id)
    {
        var enterprise = await _roleService.GetAsync(id);

        if (enterprise is null)
        {
            return NotFound();
        }

        return enterprise;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Role role)
    {
        await _roleService.CreateAsync(role);

        return CreatedAtAction(nameof(Get), new { id = role.Id }, role);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Role updatedRole)
    {
        var enterprise = await _roleService.GetAsync(id);

        if (enterprise is null)
        {
            return NotFound();
        }

        updatedRole.Id = enterprise.Id;

        await _roleService.UpdateAsync(id, updatedRole);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var role = await _roleService.GetAsync(id);

        if (role is null)
        {
            return NotFound();
        }

        await _roleService.RemoveAsync(id);

        return NoContent();
    }

}
