using Microsoft.AspNetCore.Mvc;
using UserService.Abstractions;

namespace UserService.Api;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(
        [FromBody] RegisterRequestDTO request, CancellationToken ct)
    {
        var dto = await _users.RegisterAsync(request, ct);
        return Ok(dto); // ответ: { id, email }
    }

    private readonly IUserService _users;
    public UsersController(IUserService users) => _users = users;
}