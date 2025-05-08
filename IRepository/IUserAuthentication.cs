using Microsoft.AspNetCore.Identity;
using WebAPITesting.Dtos.User;

namespace WebAPITesting.IRepository
{
    public interface IUserAuthentication
    {
        public Task<IEnumerable<IdentityError>> RegisterAsync(UserAccountDto userDto);

        public Task<bool> LoginAsync(UserLoginDto loginDto);
    }
}
