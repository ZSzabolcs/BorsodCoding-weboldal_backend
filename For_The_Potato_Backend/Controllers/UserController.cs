using For_The_Potato_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using For_The_Potato_Backend.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using For_The_Potato_Backend.Services;

namespace For_The_Potato_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllData()
        {
            var response = await _user.GetAllData();
            return Ok(response);
        }


        [HttpGet("Statistic/{name}")]
        public async Task<ActionResult> UserStatistic(string name)
        {
            var response = await _user.GetUserStatistic(name);
            return Ok(response);
        }

        [HttpGet("ToWPF")]
        public async Task<ActionResult> GetAllUserToWPF()
        {
            var response = await _user.GetAllDataToWPF();
            return Ok(response);

        }

        [HttpGet("Fiok/{name}")]
        public async Task<ActionResult> GetOneUser(string name)
        {
            var response = await _user.GetOneUserData(name);
            return Ok(response);
        }


        [HttpPost("Login")]
        public async Task<ActionResult> LoginUser([FromBody] LoginDto loginUser)
        {
            var response = await _user.PostLoginUser(loginUser);
            var responseDto = response as ResponseDto;
            if (responseDto.Value != null)
            {
                return Ok(responseDto);
            }

            return NotFound(responseDto);
        }

        [HttpPost("Registration")]
        public async Task<ActionResult> RegistUser([FromBody] UserDto registUser)
        {
            var response = await _user.PostRegistUser(registUser);
            var responseDto = response as ResponseDto;
            if (responseDto.Value != null)
            {
                return Ok(responseDto);
            }

            return BadRequest(responseDto);
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteRegistData(Guid id)
        {
            var response = await _user.DeleteData(id);
            var responseDto = response as ResponseDto;
            if (responseDto.Value != null)
            {
                return StatusCode(201, responseDto);
            }

            return NotFound(responseDto);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserData([FromBody] UserDto user)
        {
            var response = await _user.PutData(user);
            var responseDto = response as ResponseDto;
            if (responseDto.Value != null)
            {
                return StatusCode(201, responseDto);
            }

            return NotFound(responseDto);

        }

    }
}
        