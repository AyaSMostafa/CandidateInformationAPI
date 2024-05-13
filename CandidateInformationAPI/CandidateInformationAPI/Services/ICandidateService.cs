// ICandidateService.cs
using System.Threading.Tasks;
using CandidateInformationAPI.DTOs;
using CandidateInformationAPI.Models;

namespace CandidateInformationAPI.Services
{
    public interface ICandidateService
    {
        Task<CandidateDto> AddOrUpdateCandidateAsync(CandidateDto candidateDto);
    }

}
