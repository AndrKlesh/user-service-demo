using UserService.Abstractions;

namespace UserService.Implementation;

public class UserService : IUserService
{
    public async Task<UserDto> RegisterAsync(RegisterRequestDTO request, CancellationToken ct)
    {
        await Task.Delay(100, ct);
        return new UserDto(Guid.NewGuid(), request.Email, request.FullName);
    }
}