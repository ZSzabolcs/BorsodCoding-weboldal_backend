using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;


namespace For_The_Potato_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        [HttpGet("download")]
        public IActionResult DownloadFile()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Setups", "For The Potato Setup.exe");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new { message = "A kért fájl nem található." });
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            return File(fileStream, contentType, "For The Potato Setup.exe");
        }
    }
}
