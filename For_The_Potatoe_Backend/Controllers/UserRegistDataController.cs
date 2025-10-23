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

                var userData = users.Select(u => new { u.Id, u.Name, u.Password, u.Date });

                if (userData != null)
                {
                    return Ok(userData);
                }
                return BadRequest(new { message = "Sikertelen lekérdezés" });
            }

        }

        [HttpPost]
        public ActionResult<UserColumns> InsertRegistData()
        {
            return BadRequest(new { message = "Sikertelen feltöltés"});
        }
    }
}
