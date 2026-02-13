using AuthApi.Services.Dtos;

namespace For_The_Potato_Backend.Services.Interfaces
{
    public interface ISend
    {
        void SendMail(SendMailDto sendMailDto);
    }
}
