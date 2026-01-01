using For_The_Potato_Backend.Models;
using For_The_Potato_Backend.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace For_The_Potato_Backend.Services
{
    public class UserService : IUser
    {
        private readonly ForThePotatoContext _context;
        private readonly ResponseDto _responseDto;

        public UserService(ForThePotatoContext context, ResponseDto responseDto)
        {
            _context = context;
            _responseDto = responseDto;
        }

        public async Task<object> DeleteData(Guid id)
        {
            try
            {
                var record = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (record != null)
                {
                    _context.Remove(record);
                    await _context.SaveChangesAsync();
                    _responseDto.Value = record;
                    _responseDto.Message = "Sikeres törlés";
                    return _responseDto;
                }
                _responseDto.Value = null;
                _responseDto.Message = "Sikertelen törlés";
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Value = null;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }

        public async Task<object> GetAllData()
        {
            try
            {
                var users = await _context.Users.ToArrayAsync();
                _responseDto.Message = "Sikeres lekérés";
                _responseDto.Value = users;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.Value = null;
                return _responseDto;
            }
        }

        public async Task<object> GetAllDataToWPF()
        {
            try
            {
                var users = await _context.Users.Select(u => new { u.Id, u.Name, u.Password, u.RegDate, u.ModDate, u.Email }).ToArrayAsync();

                if (users != null)
                {
                    _responseDto.Message = "Sikeres lekérés";
                    _responseDto.Value = users;
                    return _responseDto;
                }
                _responseDto.Message = "Sikertelen lekérdezés";
                _responseDto.Value = null;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.Value = null;
                return _responseDto;
            }
        }

        public async Task<object> GetOneUserData(string name)
        {
            try
            {
                var oneUser = await _context.Users
                    .Select(u => new { u.RegDate, u.ModDate, u.Password, u.Email, u.Name})
                    .Where(u => u.Name == name)
                    .FirstOrDefaultAsync();
;

                if (oneUser != null)
                {
                    _responseDto.Message = "Sikeres lekérdezés";
                    _responseDto.Value = oneUser;
                    return _responseDto;
                }

                _responseDto.Message = "Sikertelen lekérdezés";
                _responseDto.Value = null;
                return _responseDto;

                
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.Value = null;
                return _responseDto;
            }
        }

        public async Task<object> GetUserStatistic(string name)
        {
            try
            {
                var userStatictic = await _context.Users
                    .Include(u => u.Save)
                    .Where(t => t.Save != null && t.Name == name)
                    .Select(t => new
                    {
                        t.Name,
                        t.Save.Points,
                        t.Save.Level,
                        t.Save.Language,
                        t.Save.RegDate,
                        t.Save.ModDate,
                        NyelvArany = _context.Nyelvaranies.FirstOrDefault(ny => ny.Language == t.Save.Language),
                        PontArany = _context.Pontaranyegyts.FirstOrDefault(p => p.Points == t.Save.Points),
                        SzintArany = _context.Szintaranies.FirstOrDefault(sz => sz.Level == t.Save.Level),
                    })
                    .FirstOrDefaultAsync();

                if (userStatictic != null)
                {
                    _responseDto.Message = "Sikeres lekérés";
                    _responseDto.Value = userStatictic;
                    return _responseDto;
                }
                
                 _responseDto.Message = "Még nincsen mentése";
                 _responseDto.Value = null;
                 return _responseDto;
                

                
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.Value = null;
                return _responseDto;
            }
        }


        public async Task<object> PostLoginUser(LoginDto loginUser)
        {
            try
            {
                if (loginUser != null)
                {

                    var foundUser = await _context.Users.FirstOrDefaultAsync(u => (u.Name == loginUser.Name || u.Email == loginUser.Name) && u.Password == loginUser.Password);

                    if (foundUser != null)
                    {
                        _responseDto.Message = "Sikeres bejelentkezés";
                        _responseDto.Value = loginUser;
                        return _responseDto;
                    }


                }

                _responseDto.Message = "Sikertelen bejelentkezés";
                _responseDto.Value = null;
                return _responseDto;
            }

            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.Value = null;
                return _responseDto;
            }
        }
        

        public async Task<object> PostRegistUser(UserDto registUser)
        {
            try
            {
                if (registUser != null)
                {

                    var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == registUser.Name && u.Email == registUser.Email && u.Password == registUser.Password);

                    if (foundUser != null)
                    {
                        _responseDto.Message = "A fiók már létezik";
                        _responseDto.Value = null;
                        return _responseDto;
                    }

                    User newUser = new User()
                    {
                        Name = registUser.Name,
                        Password = registUser.Password,
                        Email = registUser.Email
                    };

                    await _context.Users.AddAsync(newUser);
                    await _context.SaveChangesAsync();
                    _responseDto.Message = "A fiók sikeresen létrehozva";
                    _responseDto.Value = registUser;
                    return _responseDto;

                }

                _responseDto.Message = "Sikertelen bejelentkezés";
                _responseDto.Value = null;
                return _responseDto;
            }

            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.Value = null;
                return _responseDto;
            }
        }

        public async Task<object> PutData(UserDto user)
        {
            try
            {
                if (user != null)
                {
                    var getUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == user.Name);

                    if (getUser != null)
                    {
                        getUser.Password = user.Password;
                        getUser.Email = user.Email;
                        getUser.ModDate = DateTime.Now;
                        _context.Users.Update(getUser);
                        await _context.SaveChangesAsync();
                        _responseDto.Message = "Sikeres módosítás";
                        _responseDto.Value = user;
                        return _responseDto;
                    }

                }
                _responseDto.Message = "Sikertelen módosítás";
                _responseDto.Value = null;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.Value = null;
                return _responseDto;
            }
        }
    }
}
