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

        public Task<object> DeleteData()
        {
            throw new NotImplementedException();
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

        public Task<object> PutData()
        {
            throw new NotImplementedException();
        }
    }
}
