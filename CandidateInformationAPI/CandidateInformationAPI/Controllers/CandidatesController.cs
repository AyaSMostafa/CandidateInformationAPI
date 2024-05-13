using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CandidateInformationAPI.DTOs;
using CandidateInformationAPI.Models;
using CandidateInformationAPI.Services;

namespace CandidateInformationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService ?? throw new ArgumentNullException(nameof(candidateService));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDto candidateDto)
        {
            try
            {
                if (candidateDto == null)
                {
                    return BadRequest("Candidate data is missing.");
                }

                var savedCandidateDto = await _candidateService.AddOrUpdateCandidateAsync(candidateDto);

                return Ok(savedCandidateDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
