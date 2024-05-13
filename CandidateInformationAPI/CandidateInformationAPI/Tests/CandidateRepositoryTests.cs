using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using CandidateInformationAPI.Data;
using CandidateInformationAPI.Models;
using CandidateInformationAPI.Repositories;
using Moq;

namespace CandidateInformationAPI.Tests
{
    public class CandidateRepositoryTests
    {
        [Fact]
        public async Task AddOrUpdateCandidateAsync_NewCandidate_AddsCandidateToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CandidateDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new CandidateDbContext(options))
            {
                var repository = new CandidateRepository(context);
                var candidate = new Candidate
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com"
                };

                // Act
                var result = await repository.AddOrUpdateCandidateAsync(candidate);

                // Assert
                Assert.NotNull(result);
                Assert.NotEqual(0, result.CandidateId);
                Assert.Equal("John", result.FirstName);
                Assert.Equal("Doe", result.LastName);
                Assert.Equal("john.doe@example.com", result.Email);
            }
        }

        [Fact]
        public async Task AddOrUpdateCandidateAsync_ExistingCandidate_UpdatesCandidateInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CandidateDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new CandidateDbContext(options))
            {
                var existingCandidate = new Candidate
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane.doe@example.com"
                };
                context.Candidates.Add(existingCandidate);
                context.SaveChanges();

                var repository = new CandidateRepository(context);
                existingCandidate.LastName = "Smith"; // Modify existing candidate

                // Act
                var result = await repository.AddOrUpdateCandidateAsync(existingCandidate);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(existingCandidate.CandidateId, result.CandidateId);
                Assert.Equal("Jane", result.FirstName); // Ensure first name remains unchanged
                Assert.Equal("Smith", result.LastName); // Ensure last name is updated
                Assert.Equal("jane.doe@example.com", result.Email); // Ensure email remains unchanged
            }
        }
    }
}
