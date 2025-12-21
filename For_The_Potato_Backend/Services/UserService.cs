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
    }
}
