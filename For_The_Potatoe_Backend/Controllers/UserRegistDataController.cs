using For_The_Potatoe_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using For_The_Potatoe_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace For_The_Potatoe_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistData : ControllerBase
    {
        [HttpGet]
        public ActionResult<UserColumns> GetAllData()
        {
            using (var context = new For_The_PotatoeDbContext())
            {
                var users = context.User.ToList();


                if (users != null)
                {
                    return Ok(users);
                }
                return BadRequest(new { message = "Sikertelen lekérdezés" });
            }

        }
    }
}
