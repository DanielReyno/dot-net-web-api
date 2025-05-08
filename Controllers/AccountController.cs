using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPITesting.Dtos.User;
using WebAPITesting.IRepository;

namespace WebAPITesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAuthentication _userManager;

        public AccountController(IUserAuthentication userManager)
        {
            this._userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Resgister([FromBody] UserAccountDto userDto)
        {
            var registerResult = await _userManager.RegisterAsync(userDto);

            if(registerResult.Any())
            {
                foreach(var error in registerResult)
                {
                    ModelState.AddModelError(error.Code, error.Description);

                }
                return BadRequest(ModelState);
            }

            return Ok();

        }
    }
}
