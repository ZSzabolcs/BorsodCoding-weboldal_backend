using AuthApi.Services.Dtos;

namespace AuthApi.Services.Interfaces.IForThePotato
{
    public interface ISave
    {
        Task<object> GetAllData();
        Task<object> GetStatistic(string username);
        Task<object> PostData(SaveDto save);
        Task<object> PutData(SaveDto save);
        Task<object> PutDataFromWPF(SaveDtoFromWPF save);
        Task<object> DeleteData(string id);

        
    }
}
