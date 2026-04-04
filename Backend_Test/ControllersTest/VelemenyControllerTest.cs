using AuthApi.Controllers;
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
    public class VelemenyControllerTest
    {
        private static List<Velemeny> GetSampleVelemenyList() =>
        [
            new(){Id = "jel1", Ertekeles = "5", Megjegyzes = "Érdekes játék" },
            new(){Id = "jel2", Ertekeles = "1", Megjegyzes = "Nagyon rossz" }

        ];
        private static Velemeny GetSampleVelemeny() => new Velemeny() { Id = "jel1", Ertekeles = "3", Megjegyzes = "Nem rossz" };
        private static VelemenyDto GetSampleVelemenyDto() => new VelemenyDto() { Ertekeles = "3", Megjegyzes = "Szöveg", UserName = "valaki" };
        private static ResponseDto GetSampleVelemenyCollection() => new ResponseDto() { Message = "Sikeres lekérés", Value = GetSampleVelemenyList() };
        private static ResponseDto DeleteSuccessfullSample() => new ResponseDto() { Message = "Sikeres törlés", Value = GetSampleVelemeny() };
        private static ResponseDto PutSuccessfullSample() => new ResponseDto() { Message = "A vélemény sikeresen módosítva", Value = new { Ertekeles = "3", Megjegyzes = "Szöveg" } };
        private static ResponseDto PostSuccessfullSample() => new ResponseDto() { Message = "Sikeres mentés", Value = GetSampleVelemenyDto() };

        [Fact]
        public async Task GetAll_ShouldReturn200WithAllVelemeny()
        {
            var mockService = new Mock<IVelemeny>();

            mockService
                .Setup(s => s.GetAll())
                .ReturnsAsync(GetSampleVelemenyCollection());

            var controller = new VelemenyController(mockService.Object);

            var actionResult = await controller.GetAll();

            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);

            var response = okResult.Value.Should().BeAssignableTo<ResponseDto>().Subject;
            (response.Value as List<Velemeny>).Should().HaveCount(2);
            (response.Value as List<Velemeny>)[1].Ertekeles.Should().Be("1");
        }

        [Fact]
        public async Task DeleteVelemeny_ShouldReturn200WithDeletedVelemeny()
        {
            var mockService = new Mock<IVelemeny>();
            mockService
                .Setup(s => s.DeleteVelemeny(It.IsAny<string>()))
                .ReturnsAsync(DeleteSuccessfullSample());

            var controller = new VelemenyController(mockService.Object);

            var actionResult = await controller.DeleteVelemeny(It.IsAny<string>());

            var result = actionResult.Should().BeOfType<ObjectResult>().Subject;
            result.StatusCode.Should().Be(200);

            var response = result.Value.Should().BeAssignableTo<ResponseDto>().Subject;
            response.Value.Should().BeAssignableTo<Velemeny>();
            (response.Value as Velemeny).Ertekeles.Should().Be("3");

        }

        [Fact]
        public async Task PutVelemeny_ShouldReturn201WithVelemenyDto()
        {
            var mockService = new Mock<IVelemeny>();
            mockService
                .Setup(s => s.UpdateVelemeny(It.IsAny<VelemenyDto>()))
                .ReturnsAsync(PutSuccessfullSample());

            var controller = new VelemenyController(mockService.Object);

            var actionResult = await controller.UpdateVelemeny(GetSampleVelemenyDto());

            var result = actionResult.Should().BeOfType<ObjectResult>().Subject;
            result.StatusCode.Should().Be(201);

            var response = result.Value.Should().BeAssignableTo<ResponseDto>().Subject;
            response.Value.Should().Be(new {Ertekeles = "3", Megjegyzes = "Szöveg" });

        }

        [Fact]
        public async Task PostVelemeny_ShouldReturn201WithVelemeny()
        {
            var mockService = new Mock<IVelemeny>();
            mockService
                .Setup(s => s.PostVelemeny(It.IsAny<VelemenyDto>()))
                .ReturnsAsync(PostSuccessfullSample());

            var controller = new VelemenyController(mockService.Object);

            var actionResult = await controller.PostVelemeny(GetSampleVelemenyDto());

            var result = actionResult.Should().BeOfType<ObjectResult>().Subject;
            result.StatusCode.Should().Be(201);

            var response = result.Value.Should().BeAssignableTo<ResponseDto>().Subject;
            response.Value.Should().BeAssignableTo<VelemenyDto>();
            (response.Value as VelemenyDto).Ertekeles.Should().Be("3");

        }
    }
}
    

