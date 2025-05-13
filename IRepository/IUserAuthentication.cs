using Microsoft.AspNetCore.Identity;
using WebAPITesting.Dtos.User;

namespace WebAPITesting.IRepository
{
    public interface IUserAuthentication
    {
        public Task<IEnumerable<IdentityError>> RegisterAsync(UserAccountDto userDto);

        public Task<AuthResponseDto> LoginAsync(UserLoginDto loginDto);
        public Task<string> CreateRefreshToken();
        public Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
    }
}
