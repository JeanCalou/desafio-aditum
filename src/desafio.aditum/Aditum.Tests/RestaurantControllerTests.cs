using Aditum.Challenge.Api.Controllers;
using Aditum.Challenge.Application.Interfaces;
using Aditum.Challenge.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Aditum.Tests
{
    public class RestaurantControllerTests
    {
        private readonly Mock<IRestaurantService> _mockRestaurantService;
        private readonly Mock<ICSVService> _mockCsvService;
        private readonly RestaurantController _controller;

        public RestaurantControllerTests()
        {
            _mockRestaurantService = new Mock<IRestaurantService>();
            _mockCsvService = new Mock<ICSVService>();
            _controller = new RestaurantController(_mockRestaurantService.Object, _mockCsvService.Object);
        }

        [Fact]
        public async Task GetRestaurantByHour_ReturnsBadRequest_WhenTimeFormatIsInvalid()
        {
            // Arrange
            string invalidTime = "25:00"; // Horário inválido

            // Act
            var result = await _controller.GetRestaurantByHour(invalidTime);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The input need to be on the format HH:mm!", badRequestResult.Value);
        }

        [Fact]
        public async Task GetRestaurantByHour_ReturnsOk_WhenTimeFormatIsValid()
        {

            Restaurant restaurant = new Restaurant(
                    Guid.NewGuid(),
                    "Restaurant 1",
                    DateTime.Parse("12:00"),
                    DateTime.Parse("22:00")
                );

            Restaurant restaurant2 = new Restaurant(
                    Guid.NewGuid(),
                    "Restaurant 2",
                    DateTime.Parse("12:00"),
                    DateTime.Parse("23:00")
                );

            // Arrange
            string validTime = "12:00";
            var restaurantResponse = new List<Restaurant>
            {                
                restaurant, restaurant2
            };
            _mockRestaurantService.Setup(service => service.GetAllByFilterAsync(It.IsAny<DateTime>())).ReturnsAsync(restaurantResponse);

            // Act
            var result = await _controller.GetRestaurantByHour(validTime);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(restaurantResponse, okResult.Value);
        }

        [Fact]
        public async Task ProcessCSV_ReturnsBadRequest_WhenNoFileUploaded()
        {
            // Arrange
            var fileCollection = new FormFileCollection(); // Nenhum arquivo enviado

            // Act
            var result = await _controller.ProcessCSV(fileCollection);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No file uploaded.", badRequestResult.Value);
        }

        [Fact]
        public async Task ProcessCSV_ReturnsBadRequest_WhenMoreThanOneFileUploaded()
        {
            // Arrange
            var files = new FormFileCollection
            {
                new FormFile(Stream.Null, 0, 0, "file1", "file1.csv"),
                new FormFile(Stream.Null, 0, 0, "file2", "file2.csv")
            };

            // Act
            var result = await _controller.ProcessCSV(files);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("More than 1 file uploaded", badRequestResult.Value);
        }

        [Fact]
        public async Task ProcessCSV_ReturnsBadRequest_WhenFileIsNotCSV()
        {
            // Arrange
            var files = new FormFileCollection
            {
                new FormFile(Stream.Null, 0, 0, "file", "file.txt")
            };

            // Act
            var result = await _controller.ProcessCSV(files);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid file extension.", badRequestResult.Value);
        }

        [Fact]
        public async Task ProcessCSV_ReturnsOk_WhenFileIsValidCSV()
        {
            // Arrange
            var file = new FormFile(Stream.Null, 0, 0, "validFile", "file.csv");
            var files = new FormFileCollection { file };

            Restaurant restaurant = new Restaurant(
                    Guid.NewGuid(),
                    "Restaurant 1",
                    DateTime.Parse("12:00"),
                    DateTime.Parse("22:00")
                );

            var csvData = new List<dynamic> { new { Name = "Restaurant 1", Address = "123 St" } };
            var restaurants = new List<Restaurant>
            {
                restaurant
            };

            _mockCsvService.Setup(service => service.ReadCSV(It.IsAny<Stream>())).Returns(csvData);
            _mockCsvService.Setup(service => service.ProcessCSVRestaurant(csvData)).Returns(restaurants);
            _mockRestaurantService.Setup(service => service.DeleteAllDocuments()).Returns(Task.CompletedTask);
            _mockRestaurantService.Setup(service => service.InsertMany(It.IsAny<List<Restaurant>>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.ProcessCSV(files);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(restaurants, okResult.Value);
            _mockRestaurantService.Verify(service => service.DeleteAllDocuments(), Times.Once);
            _mockRestaurantService.Verify(service => service.InsertMany(It.IsAny<List<Restaurant>>()), Times.Once);
        }
    }
}
