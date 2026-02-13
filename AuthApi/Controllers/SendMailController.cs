using AuthApi.Services.Dtos;
using For_The_Potato_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly ISend _send;

        public SendMailController(ISend send)
        {
            _send = send;
        }

        [HttpPost]
        public ActionResult SendMail(SendMailDto sendMailDTO)
        {
            _send.SendMail(sendMailDTO);
            return Ok(new { Result = "Sikeres email küldés!" });
        }
    }
}
