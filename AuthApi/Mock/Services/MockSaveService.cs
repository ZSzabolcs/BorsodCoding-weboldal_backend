using AuthApi.Mock.Repositiories;
using AuthApi.Models;
using AuthApi.Services.Dtos;
using AuthApi.Services.Interfaces.IForThePotato;

namespace AuthApi.Mock.Services
{
    public class MockSaveService : ISave
    {
        private readonly ISaveRepository _repository;

        public MockSaveService(ISaveRepository repository) 
        {
            _repository = repository;
        }

        public async Task<object> DeleteData(string id)
        {
            return await _repository.DeleteData(id);
        }

        public async Task<object> GetAllData()
        {
            return await _repository.GetAllData();
        }

        public async Task<object> GetStatistic(string username)
        {
           return await _repository.GetStatistic(username);
        }

        public async Task<object> PostData(SaveDto save)
        {
            var newSave = new SaveDto()
            {
                Language = save.Language,
                Points = save.Points,
                Level = save.Level,

            };

            return await _repository.PostData(newSave);
        }

        public async Task<object> PutData(SaveDto save)
        {
            var newSave = new SaveDto()
            {
                Name = save.Name,
                Language = save.Language,
                Points = save.Points,
                Level = save.Level,
            };

            return await _repository.PutData(newSave);
        }

        public async Task<object> PutDataFromWPF(SaveDtoFromWPF save)
        {
            var newSave = new SaveDtoFromWPF()
            {
                Id = save.Id,
                Language = save.Language,
                Level = save.Level,
                Points = save.Points
            };

            return await _repository.PutDataFromWPF(newSave);
        }
    }
}
