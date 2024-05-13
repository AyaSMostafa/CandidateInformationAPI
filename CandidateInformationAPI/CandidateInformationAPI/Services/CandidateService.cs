// CandidateService.cs
using System;
using System.Threading.Tasks;
using AutoMapper;
using CandidateInformationAPI.DTOs;
using CandidateInformationAPI.Models;
using CandidateInformationAPI.Repositories;

namespace CandidateInformationAPI.Services
{ 
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;
        private ICandidateRepository @object;

        public CandidateService(ICandidateRepository @object)
        {
            this.@object = @object;
        }

        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository ?? throw new ArgumentNullException(nameof(candidateRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CandidateDto> AddOrUpdateCandidateAsync(CandidateDto candidateDto)
        {
            if (candidateDto == null)
            {
                throw new ArgumentNullException(nameof(candidateDto));
            }

            // Map DTO to entity
            var candidate = _mapper.Map<Candidate>(candidateDto);

            // Add or update candidate
            await _candidateRepository.AddOrUpdateCandidateAsync(candidate);

            // Map entity back to DTO
            var savedCandidateDto = _mapper.Map<CandidateDto>(candidate);

            return savedCandidateDto;
        }
    }
}
