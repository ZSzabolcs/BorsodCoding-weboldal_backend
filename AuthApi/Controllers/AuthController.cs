using AuthApi.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using AuthApi.Models;
using Microsoft.AspNetCore.Authorization;
using AuthApi.Services.Interfaces.IAuthService;

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
            var resp = await auth.Register(registerRequestDto);

            if (resp is string)
            {
                return BadRequest(resp);
            }
            return StatusCode(201, resp);

        }


        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(LoginRequestDto loginRequestDto)
        {
            var res = await auth.Login(loginRequestDto);

            if (res is string)
            {
                return NotFound(res);

            }

            return Ok(res);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost("assignrole")]
        public async Task<ActionResult> AddRole(string UserName, string roleName)
        {
            var res = await auth.AssignRole(UserName, roleName);

            if (res is string)
            {
                return BadRequest(res);
            }
            return Ok(res);

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAnUser(string id)
        {
            var response = await auth.DeleteUserData(id);
            if (response is string)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [Authorize(Roles = "Admin,Player")]
        [HttpGet("Fiok/{userName}")]
        public async Task<ActionResult> GetOneUserData(string userName)
        {
           var response = await auth.GetOneUserData(userName);
           if (response is string)
           {
                return NotFound(response);
           }

           return Ok(response);

        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var response = await auth.GetAllUser();
            return Ok(response);

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("FiokById/{id}")]
        public async Task<ActionResult> GetOneUserDataById(string id)
        {
            var response = await auth.GetOneUserDataById(id);
            if (response is string)
            {
                return NotFound(response);
            }

            return Ok(response);

        }
        [Authorize(Roles = "Admin,Player")]
        [HttpPut("Modositas")]
        public async Task<ActionResult> UpdateUserData(RegisterRequestDto updateUserDto)
        {
            var response = await auth.UpdateUserData(updateUserDto);
            if (response is string)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }
    }
}
