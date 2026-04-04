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
        private static ResponseDto PostSampleResponseDto() => new ResponseDto() { Message = "Sikeres mentés", Value = new SaveDto() {  } };

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


            mockRepo.Verify(repo => repo.GetAllData(), Times.Once);

        }

        [Fact]
        public async Task PostSaveAsync_ShouldBeSaveDto()
        {

            var mockRepo = new Mock<ISaveRepository>();

            mockRepo
                .Setup(repo => repo.PostData(It.IsAny<SaveDto>()))
                .ReturnsAsync((SaveDto s) => {  return s; });

            var service = new MockSaveService(mockRepo.Object);


            var result = await service.PostData(new SaveDto()
            {
                Language = "hu", Level = 2,
                Points = 400, Name = "valaki"
            });

            (result as SaveDto).Points.Should().Be(400);


            mockRepo.Verify(repo => repo.PostData(It.IsAny<SaveDto>()), Times.Once);

        }
    }
}