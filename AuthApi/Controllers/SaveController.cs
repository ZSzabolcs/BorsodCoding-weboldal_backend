using AuthApi.Models;
using AuthApi.Services.Dtos;
using AuthApi.Services.Interfaces.IForThePotato;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveController : ControllerBase, ISave
    {
        private readonly ISave _save;

        public SaveController(ISave save)
        {
            _save = save;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<object> GetAllData()
        {
            var data = await _save.GetAllData();
            return Ok(data);
        }
        [Authorize(Roles = "Admin,Player")]
        [HttpGet("Statisztika/{username}")]
        public async Task<object> GetStatistic(string username)
        {
            var response = await _save.GetStatistic(username);
            if (response is string)
            {
                NotFound(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin,Player")]
        [HttpPost]
        public async Task<object> PostData(SaveDto save)
        {
            var response = await _save.PostData(save);
            var responseDto = response as ResponseDto;
            if (responseDto.Value != null)
            {
                return StatusCode(201, responseDto);
            }
            return BadRequest(responseDto);
        }
        [Authorize(Roles = "Admin,Player")]
        [HttpPut]
        public async Task<object> PutData(SaveDto save)
        {
            var response = await _save.PutData(save);
            var responseDto = response as ResponseDto;
            if (responseDto.Value != null)
            {
                return StatusCode(201, responseDto);
            }

            return NotFound(responseDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("FromWPF")]
        public async Task<object> PutDataFromWPF(SaveDtoFromWPF save)
        {
            var response = await _save.PutDataFromWPF(save);
            var responseDto = response as ResponseDto;
            if (responseDto.Value != null)
            {
                return StatusCode(201, responseDto);
            }

            return NotFound(responseDto);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<object> DeleteData(string id)
        {
            var response = await _save.DeleteData(id);
            var responseDto = response as ResponseDto;
            if (responseDto.Value != null)
            {
                return StatusCode(201, responseDto);
            }

            return NotFound(responseDto);
        }
    }

}
