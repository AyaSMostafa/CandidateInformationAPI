using AutoMapper;
using CandidateInformationAPI.DTOs;
using CandidateInformationAPI.Models;

namespace CandidateInformationAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CandidateDto, Candidate>(); // Define mapping from CandidateDto to Candidate
            CreateMap<Candidate, CandidateDto>(); // Define mapping from Candidate to CandidateDto
        }
    }
}
