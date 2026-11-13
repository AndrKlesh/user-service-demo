namespace UserService.Abstractions;

public interface IUserService
{
    Task<UserDto> RegisterAsync(RegisterRequestDTO request, CancellationToken ct);
}