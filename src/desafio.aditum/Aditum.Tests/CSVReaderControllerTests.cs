using Aditum.Challenge.Api.Controllers;
using Aditum.Challenge.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace Aditum.Tests
{
    public class CSVReaderControllerTests
    {
        private readonly Mock<ICSVService> _mockCsvService;
        private readonly CSVReaderController _controller;

        public CSVReaderControllerTests()
        {
            _mockCsvService = new Mock<ICSVService>();
            _controller = new CSVReaderController(_mockCsvService.Object);
        }

        [Fact]
        public async Task ReadCSV_ReturnsBadRequest_WhenNoFileUploaded()
        {
            // Arrange
            var fileCollection = new FormFileCollection();

            // Act
            var result = await _controller.ReadCSV(fileCollection);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No file uploaded.", badRequestResult.Value);
        }

        [Fact]
        public async Task ReadCSV_ReturnsBadRequest_WhenMoreThanOneFileUploaded()
        {
            // Arrange
            var files = new FormFileCollection
            {
                new FormFile(Stream.Null, 0, 0, "file1", "file1.csv"),
                new FormFile(Stream.Null, 0, 0, "file2", "file2.csv")
            };

            // Act
            var result = await _controller.ReadCSV(files);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("More than 1 file uploaded", badRequestResult.Value);
        }

        [Fact]
        public async Task ReadCSV_ReturnsBadRequest_WhenFileIsEmpty()
        {
            // Arrange
            var file = new FormFile(Stream.Null, 0, 0, "emptyFile", "file.csv");

            var files = new FormFileCollection { file };

            // Act
            var result = await _controller.ReadCSV(files);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The file is empty!", badRequestResult.Value);
        }

        [Fact]
        public async Task ReadCSV_ReturnsBadRequest_WhenFileIsNotCSV()
        {
            // Arrange
            var files = new FormFileCollection
            {
                new FormFile(Stream.Null, 0, 0, "file", "file.txt")
            };

            // Act
            var result = await _controller.ReadCSV(files);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid file extension.", badRequestResult.Value);
        }

        [Fact]
        public async Task ReadCSV_ReturnsOk_WhenFileIsValidCSV()
        {
            // Arrange
            var file = new FormFile(Stream.Null, 0, 1, "validFile", "file.csv");
            var files = new FormFileCollection { file };

            var expectedData = new List<dynamic> { new { Column1 = "Data1", Column2 = "Data2" } };
            _mockCsvService.Setup(service => service.ReadCSV(It.IsAny<Stream>())).Returns(expectedData);

            // Act
            var result = await _controller.ReadCSV(files);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedData, okResult.Value);
        }
    }
}