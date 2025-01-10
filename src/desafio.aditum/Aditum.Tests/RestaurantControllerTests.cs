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
            Assert.Equal(restaurantResponse.Select(x => x.Name), okResult.Value);
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
        public async Task ProcessCSV_ValidFile_ReturnsOk()
        {
            Restaurant restaurant = new Restaurant(
                    Guid.NewGuid(),
                    "Restaurant 1",
                    DateTime.Parse("12:00"),
                    DateTime.Parse("22:00")
                );

            // Arrange
            var file = new FormFile(new MemoryStream(), 0, 0, "file", "file.csv");
            var files = new FormFileCollection { file };

            // Mock do comportamento do serviço
            _mockCsvService.Setup(s => s.ReadCSV(It.IsAny<Stream>())).Returns(new List<string> { "Data1", "Data2" });
            _mockCsvService.Setup(s => s.ProcessCSVRestaurant(It.IsAny<List<string>>())).Returns(new List<Restaurant> { restaurant });

            // Mock do serviço de restaurante
            _mockRestaurantService.Setup(s => s.DeleteAllDocuments()).Returns(Task.CompletedTask);
            _mockRestaurantService.Setup(s => s.InsertMany(It.IsAny<List<Restaurant>>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.ProcessCSV(files);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
