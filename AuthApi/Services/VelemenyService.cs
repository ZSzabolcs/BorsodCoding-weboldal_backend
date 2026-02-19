using AuthApi.Models;
using AuthApi.Datas;
using AuthApi.Services.Dtos;
using AuthApi.Services.Interfaces.IForThePotato;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Services
{
    public class VelemenyService : IVelemeny
    {
        private readonly AppDbContext _context;
        private readonly ResponseDto _responseDto;

        public VelemenyService(AppDbContext context, ResponseDto responseDto)
        {
            _context = context;
            _responseDto = responseDto;
        }

        public async Task<object> DeleteFromWPFVelemeny(string id)
        {
            try
            {
                var record = await _context.Velemeny.FirstOrDefaultAsync(v => v.Id == id);

                if (record != null)
                {
                    _context.Remove(record);
                    await _context.SaveChangesAsync();
                    _responseDto.Value = record;
                    _responseDto.Message = "Sikeres törlés";
                    return _responseDto;
                }

                return "Sikertelen törlés";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        public async Task<object> DeleteVelemeny(string userName)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(s => s.NormalizedUserName == userName.ToUpper());

                if (user != null)
                {
                    var record = await _context.Velemeny.FirstOrDefaultAsync(v => v.Id == user.Id);

                    if (record != null)
                    {
                        _context.Remove(record);
                        await _context.SaveChangesAsync();
                        _responseDto.Value = record;
                        _responseDto.Message = "Sikeres törlés";
                        return _responseDto;
                    }

                }
                return "Sikertelen törlés";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<object> GetAll()
        {
            try
            {
                var Velemeny = await _context.Velemeny.ToArrayAsync();
                _responseDto.Message = "Sikeres lekérés";
                _responseDto.Value = Velemeny;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                return _responseDto;
            }
        }

        public async Task<object> GetAVelemeny(string userName)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(s => s.NormalizedUserName == userName.ToUpper());

                if (user != null) 
                {
                    var velemeny = await _context.Velemeny.FirstOrDefaultAsync(v => v.Id == user.Id);

                    if (velemeny != null)
                    {
                        _responseDto.Message = "Sikeres lekérés";
                        _responseDto.Value = new { velemeny.Ertekeles, velemeny.Megjegyzes };
                        return _responseDto;
                    }


                    return "Még nem adtál véleményt!";
                }

                return "Nem létezik ilyen fiók";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<object> PostVelemeny(VelemenyDto velemenyDto)
        {
            try
            {
                if (velemenyDto != null)
                {

                    var user = await _context.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == velemenyDto.UserName.ToUpper() && u.Velemeny == null);

                    if (user != null)
                    {
                        Velemeny ujVelemeny = new Velemeny()
                        {
                            Id = user.Id,
                            Ertekeles = velemenyDto.Ertekeles,
                            Megjegyzes = velemenyDto.Megjegyzes
                        };

                        await _context.AddAsync(ujVelemeny);
                        await _context.SaveChangesAsync();
                        _responseDto.Message = "A véleményed sikeresen elmentve!";
                        _responseDto.Value = new { ujVelemeny.Ertekeles, ujVelemeny.Megjegyzes };
                        return _responseDto;
                    }

                
                }

                return "Sikertelen mentés. Próbálja újra később.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<object> UpdateFromWPFVelemeny(FromWPFVelemenyDto velemenyDto)
        {
            try
            {
                if (velemenyDto != null)
                {
                    
                        var velemeny = await _context.Velemeny.FirstOrDefaultAsync(v => v.Id == velemenyDto.Id);

                        if (velemeny != null)
                        {
                            velemeny.Ertekeles = velemenyDto.Ertekeles;
                            velemeny.Megjegyzes = velemenyDto.Megjegyzes;

                            _context.Velemeny.Update(velemeny);
                            await _context.SaveChangesAsync();
                            _responseDto.Message = "A módosított vélemény sikeresen elmentve";
                            _responseDto.Value = velemenyDto;
                            return _responseDto;
                        }
                    
                }

                return "Sikertelen módosítás. Próbálja újra később.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<object> UpdateVelemeny(VelemenyDto velemenyDto)
        {
            try
            {
                if (velemenyDto != null)
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == velemenyDto.UserName.ToUpper());

                    if (user != null)
                    {
                        var velemeny = await _context.Velemeny.FirstOrDefaultAsync(v => v.Id == user.Id);

                        if (velemeny != null)
                        {
                            velemeny.Ertekeles = velemenyDto.Ertekeles;
                            velemeny.Megjegyzes = velemenyDto.Megjegyzes;

                            _context.Velemeny.Update(velemeny);
                            await _context.SaveChangesAsync();
                            _responseDto.Message = "A módosított véleményed sikeresen elmentve";
                            _responseDto.Value = velemenyDto;
                            return _responseDto;
                        }
                    }
                }

                return "Sikertelen módosítás. Próbálja újra később.";
            }
            catch (Exception ex)
            {
                return ex.Message;  
            }
        }

    }
}
