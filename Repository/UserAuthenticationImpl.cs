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
        private readonly UserManager<UserAccount> _userManager;
        private readonly IConfiguration _config;
        private UserAccount? _user;

        private const string _loginProvider = "HotelsAPI";
        private const string _refreshToken = "RefreshToken";

        public UserAuthenticationImpl(IMapper mapper,UserManager<UserAccount> manager, IConfiguration config) 
        {
            this._mapper = mapper;
            this._userManager = manager;
            this._config = config;
        }

        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);
            return newRefreshToken;
        }

        public async Task<AuthResponseDto> LoginAsync(UserLoginDto loginDto)
        {
            _user = await _userManager.FindByNameAsync(loginDto.UserName);
            var canLogin = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

            if (_user == null || canLogin == false)
            {
                return null;
            }

            var token = await _GerenateToken();
            return new AuthResponseDto
            {
                UserId = _user.Id,
                Token = token,
                RefresToken = await CreateRefreshToken()
            };
        }

        public async Task<IEnumerable<IdentityError>> RegisterAsync(UserAccountDto userDto)
        {
            var userMapped = _mapper.Map<UserAccount>(userDto);
            var registerResult = await _userManager.CreateAsync(userMapped, userDto.Password);

            if(registerResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(userMapped, "User");
                
            }

            return registerResult.Errors;
        }

        public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
        {
            var jwtSecurityHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityHandler.ReadJwtToken(request.Token);
            var username = tokenContent.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            _user = await _userManager.FindByNameAsync(username);

            if (username == null || _user.Id != request.UserId)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefresToken);
            if (isValidRefreshToken)
            {
                var token = await _GerenateToken();
                return new AuthResponseDto
                {
                    Token = token,
                    UserId = _user.Id,
                    RefresToken = await CreateRefreshToken()
                };
            }

            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }

        private async Task<string> _GerenateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwSettings:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_user);
            var rolesClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("uid",_user.Id)
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
