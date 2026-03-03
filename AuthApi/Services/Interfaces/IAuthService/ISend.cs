using AuthApi.Services.Dtos;

namespace AuthApi.Services.Interfaces.IAuthService
{
    public interface ISend
    {
        void SendMail(SendMailDto sendMailDto);
        Task<object> SendMailByUserName(SendMailByUserNameDto sendMailByUserNameDto);
    }
}
