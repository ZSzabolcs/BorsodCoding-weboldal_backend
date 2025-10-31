using For_The_Potatoe_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using For_The_Potatoe_Backend.Models;
using Microsoft.EntityFrameworkCore;
using For_The_Potatoe_Backend.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

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
        public ActionResult<UserColumns> InsertRegistData([FromBody] UserDto user)
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

        [HttpPost("Login")]
        public ActionResult<UserColumns> LoginUser([FromBody] UserDto loginUser)
        {
            using (For_The_PotatoeDbContext context = new For_The_PotatoeDbContext())
            {
                if (loginUser != null)
                {

                    var foundUser = context.User.FirstOrDefault(u => u.Name == loginUser.Name && u.Password == loginUser.Password);

                    if (foundUser != null) 
                    {
                        return Ok(new { value = loginUser });
                    }
                    else
                    {
                        return NotFound(new { message = "Nincsen fiókja"});
                    }
                }

                return BadRequest(new { message = "Sikertelen bejelentkezés" });
            }
        }

        [HttpDelete]
        public ActionResult<UserColumns> DeleteRegistData(int id) 
        {
            using (For_The_PotatoeDbContext context = new For_The_PotatoeDbContext())
            {
                var record = context.User.FirstOrDefault(u => u.Id == id);

                if (record != null)
                {
                    context.Remove(record);
                    context.SaveChanges();
                    return StatusCode(204);
                }

                return BadRequest(new { message = "Sikertelen törlés" });
            }

        }

        [HttpPut]
        public ActionResult<UserColumns> UpdateUserData([FromBody] UserDto user) 
        {
            using (For_The_PotatoeDbContext context = new For_The_PotatoeDbContext())
            {
                if (user != null)
                {
                    var getUser = context.User.FirstOrDefault(u => u.Name == user.Name);

                    if (getUser != null)
                    {
                        getUser.Password = user.Password;
                        context.User.Update(getUser);
                        context.SaveChanges();
                        return StatusCode(201, new { message = "Sikeres módosítás" });
                    }
                    else
                    {
                        return NotFound(new {message = "Nincsen fiókja"});
                    }

                }

                return BadRequest(new { message = "Sikertelen frissítés" });
            }
        }



    }
}
