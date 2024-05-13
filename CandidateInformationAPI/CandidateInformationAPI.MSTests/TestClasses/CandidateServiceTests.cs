using CandidateInformationAPI.Models;
using CandidateInformationAPI.MSTests.MockData;
using CandidateInformationAPI.Repositories;
using CandidateInformationAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace CandidateInformationAPI.MSTests.TestClasses
{
    [TestClass]
    public class CandidateServiceTests
    {
        private Mock<ICandidateRepository> _mockCandidateRepository;
        private CandidateService _candidateService;

        [TestInitialize]
        public void Setup()
        {
            _mockCandidateRepository = new Mock<ICandidateRepository>();
            _candidateService = new CandidateService(_mockCandidateRepository.Object);
        }

        [TestMethod]
        public async Task AddOrUpdateCandidateAsync_ValidCandidate_ReturnsSavedCandidateDto()
        {
            // Arrange
            var mockCandidateDto = MockCandidateData.GetMockCandidateDto();
            var savedCandidate = MockCandidateData.GetMockCandidate();
            _mockCandidateRepository.Setup(repo => repo.AddOrUpdateCandidateAsync(It.IsAny<Candidate>())).ReturnsAsync(savedCandidate);

            // Act
            var result = await _candidateService.AddOrUpdateCandidateAsync(mockCandidateDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(mockCandidateDto.FirstName, result.FirstName);
            Assert.AreEqual(mockCandidateDto.LastName, result.LastName);
        }

        [TestMethod]
        public async Task AddOrUpdateCandidateAsync_NullCandidateDto_ThrowsArgumentNullException()
        {

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _candidateService.AddOrUpdateCandidateAsync(null));
        }

    }
}
