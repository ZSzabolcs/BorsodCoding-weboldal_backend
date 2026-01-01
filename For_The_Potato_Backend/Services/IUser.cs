using For_The_Potato_Backend.Models.Dto;

namespace For_The_Potato_Backend.Services
{
    public interface IUser
    {
        Task<object> GetAllData();
        Task<object> GetUserStatistic(string name);
        Task<object> GetAllDataToWPF();
        Task<object> PostLoginUser(LoginDto loginUser);
        Task<object> PostRegistUser(UserDto registUser);
        Task<object> DeleteData(Guid id);
        Task<object> PutData(UserDto user);
        Task<object> GetOneUserData(string name);
    }
}
