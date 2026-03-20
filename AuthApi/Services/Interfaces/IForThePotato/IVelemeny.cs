using AuthApi.Services.Dtos;

namespace AuthApi.Services.Interfaces.IForThePotato
{
    public interface IVelemeny
    {
        Task<object> GetAll();
        Task<object> GetAVelemeny(string userName);
        Task<object> UpdateVelemeny(VelemenyDto velemenyDto);
        Task<object> DeleteVelemeny(string id);
        Task<object> PostVelemeny(VelemenyDto velemenyDto);
        Task<object> UpdateFromWPFVelemeny(VelemenyFromWPFDto velemenyDto);
        Task<object> DeleteFromWPFVelemeny(string id);

        

    }
}
