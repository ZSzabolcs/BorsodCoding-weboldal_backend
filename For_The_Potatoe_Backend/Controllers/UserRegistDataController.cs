using For_The_Potatoe_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using For_The_Potatoe_Backend.Models;
using Microsoft.EntityFrameworkCore;
using For_The_Potatoe_Backend.Models.Dto;

namespace For_The_Potatoe_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistData : ControllerBase
    {
        [HttpGet]
        public ActionResult<UserColumns> GetAllData()
        {
            using (For_The_PotatoeDbContext context = new For_The_PotatoeDbContext())
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
        public ActionResult<UserColumns> InsertRegistData(UserDto user)
        {
            using (For_The_PotatoeDbContext context = new For_The_PotatoeDbContext())
            {

                if (user != null)
                {
                    UserColumns newUser = new UserColumns()
                    {
                        Name = user.Name,
                        Password = user.Password,
                        Date = user.Date
                    };

                    context.User.Add(newUser);
                    context.SaveChanges();
                    return StatusCode(201, new { value = newUser });
                }

                return BadRequest(new { message = "Sikertelen feltöltés" });
            }


        }


    }
}
