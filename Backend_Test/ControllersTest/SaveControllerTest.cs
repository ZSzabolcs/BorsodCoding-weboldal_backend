using AuthApi.Controllers;
using AuthApi.Mock.Services;
using AuthApi.Models;
using AuthApi.Services.Dtos;
using AuthApi.Services.Interfaces.IForThePotato;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Test.ControllersTest
{
    public class SaveControllerTest
    {
        private static List<Save> GetSampleSaves() =>
        [
            new(){Id = "jel1", Language = "hu", Level = 3, ModDate = DateTime.MinValue, Points = 300, RegDate = DateTime.Now, },
            new(){Id = "jel2", Language = "en", Level = 2, ModDate = DateTime.Now, Points = 200, RegDate = DateTime.Now, }

        ];

        private static ResponseDto GetSampleResponseDto() => new ResponseDto { Message = "Sikeres lekérés", Value = GetSampleSaves() };
        private static ResponseDto PostSampleResponseDto() => new ResponseDto { Message = "Sikeres mentés!", Value = new SaveDto() { Language = "hu", Level = 1, Name = "valaki", Points = 400 } };
        private static ResponseDto PutSampleResponseDto() => new ResponseDto { Message = "Sikeres frissítés", Value = new SaveDto() { Language = "hu", Level = 1, Name = "valaki", Points = 400 } };

        [Fact]
        public async Task GetAll_ShouldReturn200WithSaves()
        {
            var mockService = new Mock<ISave>();

            mockService
                .Setup(s => s.GetAllData())
                .ReturnsAsync(GetSampleResponseDto());

            var controller = new SaveController(mockService.Object);

            var actionResult = await controller.GetAllData();

            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);

            var saves = okResult.Value.Should().BeAssignableTo<ResponseDto>().Subject;
            (saves.Value as List<Save>).Should().HaveCount(2);
        }

        [Fact]
        public async Task PostSave_ShouldReturn201WithSaveDto()
        {
            var mockService = new Mock<ISave>();
            var saveDto = new SaveDto() { Language = "hu", Level = 1, Name = "valaki", Points =  400};
            mockService
                .Setup(s => s.PostData(saveDto))
                .ReturnsAsync(PostSampleResponseDto());

            var controller = new SaveController(mockService.Object);

            var actionResult = await controller.PostData(saveDto);

            var result = actionResult.Should().BeOfType<ObjectResult>().Subject;
            result.StatusCode.Should().Be(201);
            
            var response = result.Value.Should().BeAssignableTo<ResponseDto>().Subject;
            response.Value.Should().BeAssignableTo<SaveDto>();
            (response.Value as SaveDto).Level.Should().Be(1);
        }
        
        [Fact]
        public async Task PutSave_ShouldReturn201WithSaveDto()
        {
            var mockService = new Mock<ISave>();
            var saveDto = new SaveDto() { Language = "hu", Level = 1, Name = "valaki", Points =  400};
            mockService
                .Setup(s => s.PutData(saveDto))
                .ReturnsAsync(PutSampleResponseDto());

            var controller = new SaveController(mockService.Object);

            var actionResult = await controller.PutData(saveDto);

            var result = actionResult.Should().BeOfType<ObjectResult>().Subject;
            result.StatusCode.Should().Be(201);
            
            var response = result.Value.Should().BeAssignableTo<ResponseDto>().Subject;
            response.Value.Should().BeAssignableTo<SaveDto>();
            (response.Value as SaveDto).Level.Should().Be(1); 
            
        }
        
    }
}
