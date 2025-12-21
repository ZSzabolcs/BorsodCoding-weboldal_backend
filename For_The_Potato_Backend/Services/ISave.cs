using For_The_Potato_Backend.Models.Dto;

namespace For_The_Potato_Backend.Services
{
    public interface ISave
    {
        Task<object> GetAllData();
        Task<object> GetAllDataToWPF();
        Task<object> PostData(SaveDto save);
        Task<object> PutData(SaveDto save);
        Task<object> DeleteData(Guid id);
    }
}
