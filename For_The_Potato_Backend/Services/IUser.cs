using For_The_Potato_Backend.Models.Dto;

namespace For_The_Potato_Backend.Services
{
    public interface IUser
    {
        Task<object> GetAllData();
        Task<object> GetAllDataToWPF();
        Task<object> PostLoginUser(UserDto loginUser);
        Task<object> PostRegistUser(UserDto registUser);
        Task<object> DeleteData(Guid id);
        Task<object> PutData(UserDto user);
    }
}
