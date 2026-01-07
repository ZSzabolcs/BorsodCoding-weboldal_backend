using For_The_Potato_Backend.Models;
using For_The_Potato_Backend.Models.Dto;
using For_The_Potato_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace For_The_Potato_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveController : ControllerBase
    {
        private readonly ISave _save;

        public SaveController(ISave save)
        {
            _save = save;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllData()
        {
            var data = await _save.GetAllData();
            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult> InsertSaveData([FromBody] SaveDto Save)
        {
            var response = await _save.PostData(Save);
            var responseDto = response as ResponseDto;
            if (responseDto.Value != null)
            {
                return StatusCode(201, responseDto);
            }
            return BadRequest(responseDto);
        }
       
        

        [HttpGet("ToWPF")]
        public async Task<ActionResult> GetAllSaveToWPF()
        {
            var response = await _save.GetAllDataToWPF();
            return Ok(response);


        }
        

       [HttpPut]
       public async Task<ActionResult> UpdateUserSave([FromBody] SaveDto saveobj)
       {
            var response = await _save.PutData(saveobj);
            var responseDto = response as ResponseDto;
            if (responseDto.Value != null)
            {
                return StatusCode(201, responseDto);
            }

            return NotFound(responseDto);

       }

        [HttpPut("FromWPF")]
        public async Task<ActionResult> UpdateSaveFromWPF([FromBody] SaveDtoFromWPF saveobj)
        {
            var response = await _save.PutDataFromWPF(saveobj);
            var responseDto = response as ResponseDto;
            if (responseDto.Value != null)
            {
                return StatusCode(201, responseDto);
            }

            return NotFound(responseDto);

        }

        [HttpDelete]
       public async Task<ActionResult> RemoveASave(Guid id)
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
