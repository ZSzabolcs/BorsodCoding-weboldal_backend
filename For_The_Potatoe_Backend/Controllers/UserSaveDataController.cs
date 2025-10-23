using For_The_Potatoe_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace For_The_Potatoe_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSaveData : ControllerBase
    {
        [HttpGet]
        public ActionResult<SaveColumns> GetAllData()
        {
            using (var context = new For_The_PotatoeDbContext())
            {
                var users = context.Save.ToList();


                if (users != null)
                {
                    return Ok(users);
                }
                return BadRequest(new { message = "Sikertelen lekérdezés" });
            }

        }
    }
}
