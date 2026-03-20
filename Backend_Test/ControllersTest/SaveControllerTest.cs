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
    }
}
