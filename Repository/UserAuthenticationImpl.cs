using AutoMapper;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using WebAPITesting.Data;
using WebAPITesting.Dtos.User;
using WebAPITesting.IRepository;

namespace WebAPITesting.Repository
{
    public class UserAuthenticationImpl : IUserAuthentication
    {
        private readonly IMapper _mapper;
        private readonly UserManager<UserAccount> _manager;
        private readonly IConfiguration _config;

        public UserAuthenticationImpl(IMapper mapper,UserManager<UserAccount> manager, IConfiguration config) 
        {
            this._mapper = mapper;
            this._manager = manager;
            this._config = config;
        }

        public async Task<AuthResponseDto> LoginAsync(UserLoginDto loginDto)
        {
            var findUser = await _manager.FindByNameAsync(loginDto.UserName);
            var canLogin = await _manager.CheckPasswordAsync(findUser, loginDto.Password);

            if (findUser == null || canLogin == false)
            {
                return null;
            }

            var token = await _GerenateToken(findUser);
            return new AuthResponseDto { UserId = findUser.Id, Token = token };
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

        private async Task<string> _GerenateToken(UserAccount user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwSettings:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _manager.GetRolesAsync(user);
            var rolesClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            var userClaims = await _manager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid",user.Id)
            }.Union(userClaims).Union(rolesClaims);

            var token = new JwtSecurityToken(
                   issuer: _config["JwSettings:Issuer"],
                   audience: _config["JwSettings:Audience"],
                   claims: claims,
                   expires: DateTime.Now.AddMinutes(Convert.ToInt32(_config["JwSettings:DurationInMinutes"])),
                   signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
