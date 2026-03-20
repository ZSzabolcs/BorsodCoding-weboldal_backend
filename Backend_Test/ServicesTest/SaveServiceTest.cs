using AuthApi.Mock.Repositiories;
using AuthApi.Mock.Services;
using AuthApi.Models;
using AuthApi.Services;
using AuthApi.Services.Dtos;
using AuthApi.Services.Interfaces.IForThePotato;
using FluentAssertions;
using Moq;

namespace Backend_Test.ServicesTest
{
    public class SaveServiceTest
    {
        private static List<Save> GetSampleSaves() =>
            [
                new(){Id = "jel1", Language = "hu", Level = 3, ModDate = DateTime.MinValue, Points = 300, RegDate = DateTime.Now, },
                new(){Id = "jel2", Language = "en", Level = 2, ModDate = DateTime.Now, Points = 200, RegDate = DateTime.Now, }

            ];

        private static ResponseDto GetSampleResponseDto() => new ResponseDto { Message = "Sikeres lekérés", Value = GetSampleSaves() };

        [Fact]
        public async Task GetAllSavesAsync_ShouldReturnAllSaves()
        {

            var mockRepo = new Mock<ISaveRepository>();

            mockRepo
                .Setup(repo => repo.GetAllData())
                .ReturnsAsync(GetSampleResponseDto());

            var service = new MockSaveService(mockRepo.Object);


            var result = await service.GetAllData();


            var saves = result as ResponseDto;
            (saves.Value as List<Save>).Should().HaveCount(2);
            (saves.Value as List<Save>).First().Id.Should().Be("jel1");


            mockRepo.Verify(repo => repo.GetAllData(), Times.Once);

        }
    }
}