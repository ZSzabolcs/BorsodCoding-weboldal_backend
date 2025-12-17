using For_The_Potato_Backend.Models;
using For_The_Potato_Backend.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace For_The_Potato_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveController : ControllerBase
    {
        private readonly ForThePotatoContext _context;

        public SaveController(ForThePotatoContext context)
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
                                Id = nincsenSave.Id

                            };
                            await _context.Saves.AddAsync(newSave);
                            await _context.SaveChangesAsync();
                            return StatusCode(201, new { message = "Sikeres mentés", value = Save });
                        }


                    }

                    return BadRequest(new { message = "Sikertelen mentés"});

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
                var saves = await _context.Saves.ToArrayAsync();

                if (saves != null)
                {
                    return Ok(saves);
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
                var saves = await _context.Saves.Select(s => new { s.Id, s.Points, s.Level, s.Language, s.RegDate, s.ModDate }).ToArrayAsync();


                if (saves != null)
                {
                    return Ok(saves);
                }
                return BadRequest(new { message = "Sikertelen lekérdezés" });
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
               
        }



        [HttpGet("GetSavesUser")]
        public async Task<ActionResult> GetSavesUser()
        {
            var tablak = await _context.Saves.Include(u => u.User).ToArrayAsync();
            return Ok(tablak);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserSave([FromBody] SaveDto saveobj)
        {
            try
            {
                if (saveobj != null)
                {
                    var user = await _context.Users.Include(u => u.Save).FirstOrDefaultAsync(us => us.Name == saveobj.Name);

                    if (user == null)
                    {
                        return NotFound(new { message = "Nincsen fiókja" });
                    }
                    if (user.Save != null)
                    {
                        user.Save.Level = saveobj.Level;
                        user.Save.Points = saveobj.Points;
                        user.Save.Language = saveobj.Language;
                        user.Save.ModDate = DateTime.Now;
                        _context.Saves.Update(user.Save);
                        await _context.SaveChangesAsync();
                        return StatusCode(201, new { message = "Sikeres frissítés" });
                    }
                    else
                    {
                        return BadRequest(new { message = "Nincsen mentése" });
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
        public async Task<ActionResult> RemoveASave(Guid id)
        {
            try
            {
                var record = await _context.Saves.FirstOrDefaultAsync(s => s.Id == id);

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
