using For_The_Potato_Backend.Models;
using For_The_Potato_Backend.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace For_The_Potato_Backend.Services
{
    public class SaveService : ISave
    {
        private readonly ForThePotatoContext _context;
        private readonly ResponseDto _responseDto;

        public SaveService(ForThePotatoContext context, ResponseDto responseDto)
        {
            _context = context;
            _responseDto = responseDto;
        }

        public async Task<object> DeleteData(Guid id)
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

        public async Task<object> GetAllData()
        {
            try
            {
                var saves = await _context.Saves.ToArrayAsync();
                _responseDto.Message = "Sikeres lekérés";
                _responseDto.Value = saves;
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
                var saves = await _context.Saves.Select(s => new { s.Id, s.Points, s.Level, s.Language, s.RegDate, s.ModDate }).ToArrayAsync();


                if (saves != null)
                {
                    _responseDto.Message = "Sikeres lekérdezés";
                    _responseDto.Value = saves;
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


        public async Task<object> PostData(SaveDto save)
        {
            try
            {
                if (save != null)
                {
                    var nincsenSave = await _context.Users
                        .Include(u => u.Save)
                        .FirstOrDefaultAsync(u => u.Name == save.Name && u.Save == null);


                    if (nincsenSave == null)
                    {
                        _responseDto.Message = "Már van mentése";
                        _responseDto.Value = null;
                        return _responseDto;
                    }
                    
                    Save newSave = new Save()
                    {
                            Points = save.Points,
                            Level = save.Level,
                            Language = save.Language,
                            Id = nincsenSave.Id

                    };
                    await _context.Saves.AddAsync(newSave);
                    await _context.SaveChangesAsync();
                    _responseDto.Message = "Sikeres mentés";
                    _responseDto.Value = save;
                    return _responseDto;
                    
                }

                _responseDto.Message = "Sikertelen mentés";
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


        public async Task<object> PutData(SaveDto save)
        {
            try
            {
                if (save != null)
                {
                    var user = await _context.Users
                        .Include(u => u.Save)
                        .FirstOrDefaultAsync(us => us.Name == save.Name);

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
                        _responseDto.Value = null;
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

        public async Task<object> PutDataFromWPF(SaveDtoFromWPF save)
        {
            try
            {
                if (save != null)
                {
                    var user = await _context.Users
                        .Include(u => u.Save)
                        .FirstOrDefaultAsync(us => us.Id == save.Id);

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
                        _responseDto.Value = null;
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
