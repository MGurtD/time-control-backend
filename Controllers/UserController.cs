using Microsoft.AspNetCore.Mvc;
using TimeControl.Models;
using TimeControl.Services;

namespace TimeControl.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly UserService _userService;

    public UserController(ILogger<UserController> logger, UserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    public async Task<List<User>> Get() =>
        await _userService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var user = await _userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(LoginForm loginForm)
    {
        var result = await _userService.CheckLoginForm(loginForm);
        switch(result) 
        {
            case LoginFormResult.NotFound:
                return NotFound($"Username {loginForm.Username} does not exist");
            case LoginFormResult.NotFoundInEnterpise:
                return NotFound($"Username {loginForm.Username} does not exist in the selected enterprise");
            case LoginFormResult.IncorrectPassword:
                return Unauthorized("Incorrect password");
            case LoginFormResult.Ok:
                var user = await _userService.GetByUsernameAsync(loginForm.Username);
                return Ok(user);
            default:
                return Problem();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(User user)
    {
        await _userService.CreateAsync(user);

        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, User updatedUser)
    {
        var enterprise = await _userService.GetAsync(id);

        if (enterprise is null)
        {
            return NotFound();
        }

        updatedUser.Id = enterprise.Id;

        await _userService.UpdateAsync(id, updatedUser);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var role = await _userService.GetAsync(id);

        if (role is null)
        {
            return NotFound();
        }

        await _userService.RemoveAsync(id);

        return NoContent();
    }

}
