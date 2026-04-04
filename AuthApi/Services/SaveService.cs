using AuthApi.Datas;
using AuthApi.Models;
using AuthApi.Services.Dtos;
using AuthApi.Services.Interfaces.IForThePotato;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Services
{
    public class SaveService : ISave
    {
        private readonly AppDbContext _context;
        private readonly ResponseDto _responseDto;

        public SaveService(AppDbContext context, ResponseDto responseDto)
        {
            _context = context;
            _responseDto = responseDto;
        }

        public async Task<object> DeleteData(string id)
        {
            try
            {
                var record = await _context.Saves.FirstOrDefaultAsync(s => s.Id == id);

                if (record != null)
                {
                    _context.Remove(record);
                    await _context.SaveChangesAsync();
                    _responseDto.Value = record;
                    _responseDto.Message = "Sikeres törlés";
                    return _responseDto;
                }
                _responseDto.Message = "Sikertelen törlés";
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }

        public async Task<object> GetAllData()
        {
            try
            {
                var saves = _context.Saves;
                _responseDto.Message = "Sikeres lekérés";
                _responseDto.Value = saves;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }



        public async Task<object> GetStatistic(string username)
        {
            try
            {
                var vanMentese = await _context.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == username.ToUpper() && u.Save != null);

                if (vanMentese != null)
                {
                    string id = vanMentese.Id;
                    var save = await _context.Saves.FirstOrDefaultAsync(s => s.Id == id);

                    var ertek = new
                    {
                        save.Points,
                        save.Level,
                        save.Language,
                        save.RegDate,
                        save.ModDate,
                        PontArany =  _context.Pontaranyegyts.FirstOrDefault(p => p.Points == save.Points).Szazalek,
                        SzintArany = _context.Szintaranies.FirstOrDefault(sz => sz.Level == save.Level).Szazalek,
                        NyelvArany = _context.Nyelvaranies.FirstOrDefault(ny => ny.Language == save.Language).Szazalek,
                        jatekosDb = _context.Menteseks.FirstOrDefault().Db

                    };
                    _responseDto.Message = "Sikeres lekérés";
                    _responseDto.Value = ertek;
                    return _responseDto;
                }

                return "Nincsen mentése!";
               
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<object> PostData(SaveDto save)
        {
            try
            {
                if (save != null)
                {
                    var user = await _context.Users
                        .FirstOrDefaultAsync(u => u.NormalizedUserName == save.Name.ToUpper());

                    if (user == null)
                    {
                        _responseDto.Message = "Nincs ilyen felhasználó";
                        return _responseDto;
                    }

                    if (user.Save == null)
                    {
                        Save newSave = new Save()
                        {
                            Points = save.Points,
                            Level = save.Level,
                            Language = save.Language,
                            Id = user.Id,
                            RegDate = DateTime.Now,
                        };
                        await _context.Saves.AddAsync(newSave);
                        await _context.SaveChangesAsync();
                        _responseDto.Message = "Sikeres mentés";
                        _responseDto.Value = save;
                        return _responseDto;
                    }
                    else
                    {
                        _responseDto.Message = "Sikertelen mentés";
                        return _responseDto;
                    }
                }

                _responseDto.Message = "Sikertelen mentés";
                return _responseDto;

            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }


        public async Task<object> PutData(SaveDto save)
        {
            try
            {
                if (save != null)
                {
                    var user = await _context.Users
                        .FirstOrDefaultAsync(us => us.NormalizedUserName == save.Name.ToUpper());

                    if (user == null)
                    {
                        _responseDto.Message = "Nincsen ilyen felhasználó";
                        return _responseDto;
                    }

                    if (user.Save != null)
                    {
                        user.Save.Level = save.Level;
                        user.Save.Points = save.Points;
                        user.Save.Language = save.Language;
                        user.Save.ModDate = DateTime.Now;
                        _context.Saves.Update(user.Save);
                        await _context.SaveChangesAsync();
                        _responseDto.Message = "Sikeres frissítés";
                        _responseDto.Value = save;
                        return _responseDto;
                    }
                    else
                    {
                        _responseDto.Message = "Nincsen mentése";
                        return _responseDto;
                    }


                }

                _responseDto.Message = "Sikertelen módosítás";
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }

        public async Task<object> PutDataFromWPF(SaveDtoFromWPF save)
        {
            try
            {
                if (save != null)
                {
                    var user = await _context.Users
                        .Include(u => u.Save)
                        .FirstOrDefaultAsync(us => us.Id == save.Id);

                    if (user == null)
                    {
                        _responseDto.Message = "Nincsen mentése";
                        return _responseDto;
                    }


                    if (user.Save != null)
                    {
                        user.Save.Level = save.Level;
                        user.Save.Points = save.Points;
                        user.Save.Language = save.Language;
                        user.Save.ModDate = DateTime.Now;
                        _context.Saves.Update(user.Save);
                        await _context.SaveChangesAsync();
                        _responseDto.Message = "Sikeres frissítés";
                        _responseDto.Value = save;
                        return _responseDto;
                    }
                    else
                    {
                        _responseDto.Message = "Nincsen mentése";
                        return _responseDto;
                    }


                }

                _responseDto.Message = "Sikertelen módosítás";
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }
    }
}
