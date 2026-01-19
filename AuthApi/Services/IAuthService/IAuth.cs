using AuthApi.Services.Dtos;

namespace AuthApi.Services.IAuthService
{
    public interface IAuth
    {
        Task<object> Register(RegisterRequestDto registerRequestDto);
        Task<object> Login(LoginRequestDto loginRequestDto);
        Task<object> AssignRole(string UserName, string roleName);

        Task<object> UpdateUserData(RegisterRequestDto updateUserDto);

        Task<object> DeleteUserData();

        Task<object> GetOneUserData(string userName);
        Task<object> GetOneUserDataById(string id);
    }
}
