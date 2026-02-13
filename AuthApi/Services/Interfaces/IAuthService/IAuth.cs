using AuthApi.Services.Dtos;

namespace AuthApi.Services.Interfaces.IAuthService
{
    public interface IAuth
    {
        Task<object> Register(RegisterRequestDto registerRequestDto);
        Task<object> Login(LoginRequestDto loginRequestDto);
        Task<object> AssignRole(string UserName, string roleName);
        Task<object> UpdateUserData(RegisterRequestDto updateUserDto);
        Task<object> DeleteUserData(string id);
        Task<object> GetOneUserData(string userName);
        Task<object> GetOneUserDataById(string id);
        Task<object> GetAllUser();
    }
}
