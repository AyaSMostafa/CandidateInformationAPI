using System.Threading.Tasks;
using CandidateInformationAPI.Models;

namespace CandidateInformationAPI.Repositories
{
    public interface ICandidateRepository
    {
        // Task<Candidate> GetCandidateByEmailAsync(string email);
        Task<Candidate> AddOrUpdateCandidateAsync(Candidate candidate);
    }
}
