using AuthApi.Services.Dtos;
using AuthApi.Services.Interfaces.IForThePotato;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VelemenyController : ControllerBase
    {
        private readonly IVelemeny _velemeny;

        public VelemenyController(IVelemeny velemeny)
        {
            _velemeny = velemeny;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var response = await _velemeny.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Player")]
        [HttpGet("{userName}")]
        public async Task<ActionResult> GetAVelemeny(string userName)
        {
            try
            {
                var response = await _velemeny.GetAVelemeny(userName);

                if (response is string)
                {
                    NotFound(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Player")]
        [HttpPost]
        public async Task<ActionResult> PostAVelemeny(VelemenyDto velemenyDto)
        {
            try
            {
                var response = await _velemeny.PostVelemeny(velemenyDto);

                if (response is string)
                {
                    return BadRequest(response);
                }

                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Player")]
        [HttpPut]
        public async Task<ActionResult> UpdateVelemeny(VelemenyDto velemenyDto)
        {
            try
            {
                var response = await _velemeny.UpdateVelemeny(velemenyDto);

                if (response is string)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("FromWPF")]
        public async Task<ActionResult> UpdateFromWPFVelemeny(FromWPFVelemenyDto velemenyDto)
        {
            try
            {
                var response = await _velemeny.UpdateFromWPFVelemeny(velemenyDto);

                if (response is string)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("FromWPF")]
        public async Task<ActionResult> DeleteFromWPFVelemeny(string id)
        {
            try
            {
                var response = await _velemeny.DeleteFromWPFVelemeny(id);

                if (response is string)
                {
                    NotFound(response);
                }

                return Ok(response);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Player")]
        [HttpDelete]
        public async Task<ActionResult> DeleteVelemeny(string userName)
        {
            try
            {
                var response = await _velemeny.DeleteVelemeny(userName);

                if (response is string)
                {
                    NotFound(response);
                }

                return StatusCode(203, response);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
