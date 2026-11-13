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
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ArgumentException("Email is required");
        if (request.Password is { Length: < 8 })
            throw new ArgumentException("Password too short");

        var dto = await _users.RegisterAsync(request, ct);
        return Ok(dto); // ответ: { id, email }
    }

    private readonly IUserService _users;
    public UsersController(IUserService users) => _users = users;
}