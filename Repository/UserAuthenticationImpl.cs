using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Net;
using WebAPITesting.Data;
using WebAPITesting.Dtos.User;
using WebAPITesting.IRepository;

namespace WebAPITesting.Repository
{
    public class UserAuthenticationImpl : IUserAuthentication
    {
        private readonly IMapper _mapper;
        private readonly UserManager<UserAccount> _manager;

        public UserAuthenticationImpl(IMapper mapper,UserManager<UserAccount> manager) 
        {
            this._mapper = mapper;
            this._manager = manager;
        }
        public async Task<IEnumerable<IdentityError>> RegisterAsync(UserAccountDto userDto)
        {
            var userMapped = _mapper.Map<UserAccount>(userDto);
            var registerResult = await _manager.CreateAsync(userMapped, userDto.Password);

            if(registerResult.Succeeded)
            {
                await _manager.AddToRoleAsync(userMapped, "User");
                
            }

            return registerResult.Errors;
        }
    }
}
