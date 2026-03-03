using AuthApi.Services.Dtos;
using AuthApi.Services.Interfaces.IAuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpPost("ByUserName")]
        public async Task<ActionResult> SendMailByUserName(SendMailByUserNameDto sendMailByUserNameDto)
        {
            var response = await _send.SendMailByUserName(sendMailByUserNameDto);
            return Ok(response);
        }
    }
}
