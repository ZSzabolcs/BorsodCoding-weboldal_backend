using For_The_Potatoe_Backend.Models;
using For_The_Potatoe_Backend.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace For_The_Potatoe_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSaveData : ControllerBase
    {
        [HttpGet]
        public ActionResult<SaveColumns> GetAllData()
        {
            using (For_The_PotatoeDbContext context = new For_The_PotatoeDbContext())
            {
                var users = context.Save.ToList();

                var userData = users.Select(s => new { s.UserId, s.Points, s.Level, s.Language, s.Date });

                if (userData != null)
                {
                    return Ok(userData);
                }
                return BadRequest(new { message = "Sikertelen lekérdezés" });
            }

        }


        [HttpPost]
        public ActionResult<SaveColumns> InsertSaveData([FromBody] InsertSaveDto Save)
        {
            using (For_The_PotatoeDbContext context = new For_The_PotatoeDbContext())
            {
                try
                {
                    if (Save != null)
                    {
                        var userId = context.User.Where(u => u.Name == Save.Name).Select(u => u.Id).FirstOrDefault();

                    if (userId == null)
                    {
                        return NotFound(new { message = "Nincsen fiókja" });
                    }

                        var getSave = context.Save.Where(s => s.UserId == userId).FirstOrDefault();

                    if (getSave != null)
                    {
                        return Ok(new { message = "Már van mentése" });
                    }

                        SaveColumns newSave = new SaveColumns()
                        {
                            Points = Save.Points,
                            Level = Save.Level,
                            Language = Save.Language,
                            Date = Save.Date,
                            UserId = userId

                        };
                        context.Save.Add(newSave);
                        context.SaveChanges();
                        return StatusCode(201, new { value = Save });
                    }

                }
                catch(DbUpdateException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
                
                return BadRequest(new { message = "Sikertelen hozzáadás" });

            }

        }

        [HttpPut]
        public ActionResult<SaveColumns> UpdateUserSave([FromBody] InsertSaveDto saveobj)
        {
            using (For_The_PotatoeDbContext context = new For_The_PotatoeDbContext())
            {
                if (saveobj != null)
                {
                    var getUser = context.User.FirstOrDefault(u => u.Name == saveobj.Name);

                    if (getUser == null)
                    {
                        return NotFound(new { message = "Nincsen fiókja" });
                    }

                    var userSave = context.Save.FirstOrDefault(s => s.UserId == getUser.Id);

                    if (userSave != null) 
                    {

                        userSave.Level = saveobj.Level;
                        userSave.Points = saveobj.Points;
                        userSave.Language = saveobj.Language;
                        userSave.Date = saveobj.Date;

                        context.Save.Update(userSave);
                        context.SaveChanges();
                        return StatusCode(201, new { message = "Sikeres frissítés" });
                        
                    }
                    else
                    {
                        return NotFound(new { message = "Nincsen mentése" });
                    }
                }

                return BadRequest(new { message = "Sikertelen frissítés"});
            }
        }

        [HttpDelete]
        public ActionResult<SaveColumns> RemoveASave(int id)
        {
            using (For_The_PotatoeDbContext context = new For_The_PotatoeDbContext())
            {
                var record = context.Save.FirstOrDefault(s => s.UserId == id);

                if (record != null)
                {
                    context.Remove(record);
                    context.SaveChanges();
                    return StatusCode(204);
                }

                return BadRequest(new { message = "Sikertelen törlés" });

            }
        } 
    }
}
