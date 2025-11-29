using For_The_Potatoe_Backend.Models;
using For_The_Potatoe_Backend.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace For_The_Potatoe_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveController : ControllerBase
    {
        private readonly ForThePotatoeContext _context;

        public SaveController(ForThePotatoeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> InsertSaveData([FromBody] SaveDto Save)
        {
             try
                {
                    if (Save != null)
                    {
                        var nincsenSave = await _context.Users.Include(u => u.Save).FirstOrDefaultAsync(u => u.Name == Save.Name && u.Save == null);


                        if (nincsenSave == null)
                        {
                            return Ok(new { message = "Már van mentése" });
                        }
                        else
                        {
                            Save newSave = new Save()
                            {
                                Points = Save.Points,
                                Level = Save.Level,
                                Language = Save.Language,
                                UserId = nincsenSave.Id

                            };
                            await _context.Saves.AddAsync(newSave);
                            await _context.SaveChangesAsync();
                            return StatusCode(201, new { message = "Sikeres mentés", value = Save });
                        }


                    }

                    return BadRequest(new {message = "Sikertelen mentés"});

                }
                catch (DbUpdateException ex)
                {
                    return BadRequest(new { message = ex.Message });
                }

        }

        [HttpGet]
        public async Task<ActionResult> GetAllData()
        {
            try
            {
                var users = await _context.Saves.ToArrayAsync();

                if (users != null)
                {
                    return Ok(users);
                }
                return BadRequest(new { message = "Sikertelen lekérdezés" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("ToWPF")]
        public async Task<ActionResult> GetAllSaveToWPF()
        {
            try
            {
                var users = await _context.Saves.ToArrayAsync();

                var userData = users.Select(s => new { s.UserId, s.Points, s.Level, s.Language });

                if (userData != null)
                {
                    return Ok(userData);
                }
                return BadRequest(new { message = "Sikertelen lekérdezés" });
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
               
        }

        [HttpGet("GetUsersSave")]
        public async Task<ActionResult> GetUsersSave()
        {
            var tablak = await _context.Users.Include(u => u.Save).ToArrayAsync();
            return Ok(tablak);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserSave([FromBody] SaveDto saveobj)
        {
            try
            {
                if (saveobj != null)
                {
                    var getUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == saveobj.Name);

                    if (getUser == null)
                    {
                        return NotFound(new { message = "Nincsen fiókja" });
                    }

                    var userSave = await _context.Saves.FirstOrDefaultAsync(s => s.UserId == getUser.Id);

                    if (userSave != null)
                    {

                        userSave.Level = saveobj.Level;
                        userSave.Points = saveobj.Points;
                        userSave.Language = saveobj.Language;

                        _context.Saves.Update(userSave);
                        await _context.SaveChangesAsync();
                        return StatusCode(201, new { message = "Sikeres frissítés" });

                    }
                    else
                    {
                        return NotFound(new { message = "Nincsen mentése" });
                    }
                }
                return BadRequest(new { message = "Sikertelen módosítás" });
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
            
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveASave(int id)
        {
            try
            {
                var record = await _context.Saves.FirstOrDefaultAsync(s => s.UserId == id);

                if (record != null)
                {
                    _context.Remove(record);
                    await _context.SaveChangesAsync();
                    return StatusCode(204);
                }

                return BadRequest(new { message = "Sikertelen törlés" });
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
               

        }
        
    }
        
}
