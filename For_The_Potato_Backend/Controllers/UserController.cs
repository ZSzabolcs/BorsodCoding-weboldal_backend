using For_The_Potato_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using For_The_Potato_Backend.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace For_The_Potato_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ForThePotatoContext _context;

        public UserController(ForThePotatoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> RegistAnUser([FromBody] UserDto user)
        {
            try
            {
                if (user != null)
                {
                    User newUser = new User()
                    {
                        Name = user.Name,
                        Password = user.Password,
                        Email = user.Email,
                    };

                    await _context.Users.AddAsync(newUser);
                    await _context.SaveChangesAsync();
                    return StatusCode(201, new { message = "Sikeres regisztráció", value = newUser });
                }

                return BadRequest(new { message = "Sikertelen feltöltés" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }

        [HttpGet]
        public async Task<ActionResult> GetAllData()
        {
            try
            {
                var users = await _context.Users.ToArrayAsync();

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

        [HttpGet("GetUsersDoNotHaveSave")]
        public async Task<ActionResult> GetUsersSave()
        {
            var nincsMentesuk = await _context.Users.Include(u => u.Save).Where(u => u.Save == null).ToArrayAsync();
            return Ok(nincsMentesuk);
        }

        [HttpGet("ToWPF")]
        public async Task<ActionResult> GetAllUserToWPF()
        {
            try
            {
                var users = await _context.Users.Select(u => new { u.Id, u.Name, u.Password, u.RegDate, u.ModDate, u.Email }).ToArrayAsync();

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

        [HttpPost("Login")]
        public async Task<ActionResult> LoginUser([FromBody] UserDto loginUser)
        {
            try
            {
                if (loginUser != null)
                {

                    var foundUser = await _context.Users.FirstOrDefaultAsync(u => (u.Name == loginUser.Name || u.Email == loginUser.Name) && u.Password == loginUser.Password);

                    if (foundUser != null)
                    {
                        return Ok(new { message = "Sikeres bejelentkezés", value = loginUser });
                    }
                    
                    
                    return NotFound(new { message = "Nincsen fiókja" });
                    
                }

                return BadRequest(new { message = "Sikertelen bejelentkezés" });
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, value = loginUser });
            }
        }

        [HttpPost("Registration")]
        public async Task<ActionResult> RegistUser([FromBody] RegistUserDto registUser)
        {
            try
            {
                if (registUser != null)
                {

                    var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == registUser.Name && u.Email == registUser.Email && u.Password == registUser.Password);

                    if (foundUser != null)
                    {
                        return BadRequest(new { message = "A fiók már létezik", value = registUser });
                    }

                    User newUser = new User() { 
                        Name = registUser.Name,
                        Password = registUser.Password,
                        Email = registUser.Email 
                    };

                    await _context.Users.AddAsync(newUser);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "A fiók sikeresen létrehozva" });

                }

                return BadRequest(new { message = "Sikertelen bejelentkezés" });
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, value = registUser });
            }
        }

        [HttpPost("CheckUserName")]
        public async Task<ActionResult> CheckUserName([FromBody] CheckUserDto checkUser)
        {
            try
            {
                if (checkUser != null)
                {
                    var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == checkUser.Name);

                    if (foundUser != null) 
                    {
                        return Ok(new { message = "A felhasználó létezik" });
                    }
                    else
                    {
                        NotFound(new { message = "A felhasználó nem létezik" });
                    }
                }
                return BadRequest(new { message = "Sikertelen lekérés" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message, value = checkUser });
            }
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteRegistData(Guid id)
        {
            try
            {
                var record = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (record != null)
                {
                    _context.Remove(record);
                    await _context.SaveChangesAsync();
                    return StatusCode(204);
                }

                return NotFound(new { message = "Sikertelen törlés" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserData([FromBody] UserDto user)
        {
            try
            {
                if (user != null)
                {
                    var getUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == user.Name);

                    if (getUser != null)
                    {
                        getUser.Password = user.Password;
                        getUser.ModDate = DateTime.Now;
                        _context.Users.Update(getUser);
                        await _context.SaveChangesAsync();
                        return StatusCode(201, new { message = "Sikeres módosítás" });
                    }

                    return NotFound(new { message = "Nincsen fiókja" });

                }

                return BadRequest(new { message = "Sikertelen módosítás" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
       
    }
}
        