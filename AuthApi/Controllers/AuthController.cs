using AuthApi.Services.Dtos;
using AuthApi.Services.IAuthService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using AuthApi.Models;

namespace AuthApi.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth auth;
        public AuthController(IAuth auth)
        {
            this.auth = auth;
        }

        [HttpPost("register")]
        public async Task<ActionResult> AddNewUser(RegisterRequestDto registerRequestDto)
        {
            var user = await auth.Register(registerRequestDto);

            if (user != null)
            {
                return StatusCode(201, user);
            }

            return BadRequest(new { result = "", message = "Sikertelen regisztráció." });
        }


        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(LoginRequestDto loginRequestDto)
        {
            var res = await auth.Login(loginRequestDto);

            if (res != null)
            {
                return StatusCode(200, res);
            }

            return NotFound(res);
        }

        [HttpPost("assignrole")]
        public async Task<ActionResult> AddRole(string UserName, string roleName)
        {
            var res = await auth.AssignRole(UserName, roleName);

            if (res != null)
            {
                return Ok(res);
            }

            return BadRequest(res);
        }

        [HttpGet("Fiok/{userName}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(Response), 404)]
        public async Task<ActionResult> GetOneUserData(string userName)
        {
           var response = await auth.GetOneUserData(userName);
           if (response is Response)
           {
                var result = (Response)response;
                return NotFound(result.Message);
           }

           return Ok(response);

        }

        [HttpPut("Modositas")]
        public async Task<ActionResult> UpdateUserData(RegisterRequestDto updateUserDto)
        {
            var response = await auth.UpdateUserData(updateUserDto);
            if (response is Response)
            {
                var result = (Response)response;
                return BadRequest(result.Message);
            }

            return Ok(response);

        }
    }
}
