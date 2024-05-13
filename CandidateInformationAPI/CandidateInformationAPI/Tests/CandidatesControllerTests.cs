using System;
using System.Threading.Tasks;
using CandidateInformationAPI.Controllers;
using CandidateInformationAPI.DTOs;
using CandidateInformationAPI.Models;
using CandidateInformationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CandidateInformationAPI.Tests
{
    public class CandidatesControllerTests
    {
        [Fact]
        public async Task AddOrUpdateCandidate_ValidDto_ReturnsOkResult()
        {
            // Arrange
            var candidateServiceMock = new Mock<ICandidateService>();
            var controller = new CandidatesController(candidateServiceMock.Object);

            var candidateDto = new CandidateDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
                // Add other properties as needed
            };

            candidateServiceMock.Setup(service => service.AddOrUpdateCandidateAsync(candidateDto))
                                 .ReturnsAsync(new CandidateDto { /* Mock saved candidate DTO */ });

            // Act
            var result = await controller.AddOrUpdateCandidate(candidateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var savedCandidate = Assert.IsType<CandidateDto>(okResult.Value);
            Assert.NotNull(savedCandidate);
            // Assert other properties as needed
        }

        [Fact]
        public async Task AddOrUpdateCandidate_NullDto_ReturnsBadRequest()
        {
            // Arrange
            var candidateServiceMock = new Mock<ICandidateService>();
            var controller = new CandidatesController(candidateServiceMock.Object);

            // Act
            var result = await controller.AddOrUpdateCandidate(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Candidate data is missing.", badRequestResult.Value);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            var candidateServiceMock = new Mock<ICandidateService>();
            var controller = new CandidatesController(candidateServiceMock.Object);

            var candidateDto = new CandidateDto();

            candidateServiceMock.Setup(service => service.AddOrUpdateCandidateAsync(candidateDto))
                                 .ThrowsAsync(new Exception("Service error message"));

            // Act
            var result = await controller.AddOrUpdateCandidate(candidateDto);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Internal server error: Service error message", statusCodeResult.Value);
        }
    }
}
