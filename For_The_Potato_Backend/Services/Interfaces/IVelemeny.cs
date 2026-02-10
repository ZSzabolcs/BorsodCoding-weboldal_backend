using For_The_Potato_Backend.Models.Dto;

namespace For_The_Potato_Backend.Services.Interfaces
{
    public interface IVelemeny
    {
        Task<object> GetAll();
        Task<object> GetAVelemeny(string userName);
        Task<object> UpdateVelemeny(VelemenyDto velemenyDto);
        Task<object> DeleteVelemeny(string id);
        Task<object> PostVelemeny(VelemenyDto velemenyDto);

          

    }
}
