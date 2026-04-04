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

        private static SaveDto GetSampleSaveDto() => new SaveDto() { Name = "valaki", Language = "hu", Level = 2, Points = 500 };

        private static ResponseDto GetSampleResponseDto() => new ResponseDto { Message = "Sikeres lekérés", Value = GetSampleSaves() };

        private static ResponseDto PostSuccesfullSampleResponseDto() => new ResponseDto() { Message = "Sikeres mentés", Value = GetSampleSaveDto() };

        private static ResponseDto PostErrorSampleResponseDto() => new ResponseDto { Message = "Sikertelen mentés", Value = null };


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
        public async Task PostSaveAsync_ShouldBeSuccesfull()
        {

            var mockRepo = new Mock<ISaveRepository>();

            mockRepo
                .Setup(repo => repo.PostData(It.IsAny<SaveDto>()))
                .ReturnsAsync(PostSuccesfullSampleResponseDto());

            var service = new MockSaveService(mockRepo.Object);


            var result = await service.PostData(GetSampleSaveDto());

            result.Should().BeAssignableTo<ResponseDto>();
            var response = (ResponseDto)result;
            response.Value.Should().BeAssignableTo<SaveDto>();
            (response.Value as SaveDto).Level.Should().Be(2);


            mockRepo.Verify(repo => repo.PostData(It.IsAny<SaveDto>()), Times.Once);

        }

        [Fact]
        public async Task PostSaveAsync_ShouldBeErrorIfSaveDtoEmpty()
        {

            var mockRepo = new Mock<ISaveRepository>();

            mockRepo
                .Setup(repo => repo.PostData(It.IsAny<SaveDto>()))
                .ReturnsAsync(PostErrorSampleResponseDto());

            var service = new MockSaveService(mockRepo.Object);


            var result = await service.PostData(new SaveDto());

            result.Should().BeAssignableTo<ResponseDto>();
            var response = result as ResponseDto;
            response.Value.Should().BeNull();

            mockRepo.Verify(repo => repo.PostData(It.IsAny<SaveDto>()), Times.Once);

        }
    }
}