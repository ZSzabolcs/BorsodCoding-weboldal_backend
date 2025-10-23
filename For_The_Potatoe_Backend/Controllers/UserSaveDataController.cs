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

                if (users != null)
                {
                    return Ok(users);
                }
                return BadRequest(new { message = "Sikertelen lekérdezés" });
            }

        }
        [HttpPost]
        public ActionResult<SaveColumns> InsertSaveData(InsertSaveDto save)
        {
            using (For_The_PotatoeDbContext context = new For_The_PotatoeDbContext())
            {
                try
                {
                    var userId = context.User.Where(u => u.Name == save.Name).Select(u => u.Id).FirstOrDefault();
                    bool vanUser = (userId != null ? true : false);

                    var getSave = context.Save.Where(s => s.UserId == userId).FirstOrDefault();
                    bool nincsSave = (getSave == null ? true : false);

                    if (vanUser && save != null && nincsSave)
                    {
                        SaveColumns newSave = new SaveColumns()
                        {
                            Points = save.Points,
                            Level = save.Level,
                            Language = save.Language,
                            Date = save.Date,
                            UserId = userId

                        };
                        context.Save.Add(newSave);
                        context.SaveChanges();
                        return StatusCode(201, new { Value = save });
                    }

                }
                catch(DbUpdateException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
                
                return BadRequest(new { message = "Már van mentése" });

            }

        }
    }
}
