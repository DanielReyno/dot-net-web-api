﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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


        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userDto)
        {
            var authResponse = await _userManager.LoginAsync(userDto);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);

        }

        [HttpPost]
        [Route("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDto request)
        {
            var authResponse = await _userManager.VerifyRefreshToken(request);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);

        }
    }
}
