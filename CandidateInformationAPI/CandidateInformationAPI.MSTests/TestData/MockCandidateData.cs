using CandidateInformationAPI.DTOs;
using CandidateInformationAPI.Models;

namespace CandidateInformationAPI.MSTests.MockData
{
    public static class MockCandidateData
    {
        public static Candidate GetMockCandidate()
        {
            return new Candidate
            {
                CandidateId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890",
                LinkedInUrl = "https://linkedin.com/in/johndoe",
                GitHubUrl = "https://github.com/johndoe",
                FreeTextComment = "Test comment"
            };
        }

        public static CandidateDto GetMockCandidateDto()
        {
            return new CandidateDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890",
                LinkedInUrl = "https://linkedin.com/in/johndoe",
                GitHubUrl = "https://github.com/johndoe",
                FreeTextComment = "Test comment"
            };
        }
    }
}
