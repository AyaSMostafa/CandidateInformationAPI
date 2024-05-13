using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CandidateInformationAPI.Data;
using CandidateInformationAPI.Models;

namespace CandidateInformationAPI.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CandidateDbContext _context;

        public CandidateRepository(CandidateDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Candidate> AddOrUpdateCandidateAsync(Candidate candidate)
        {
            if (candidate == null)
            {
                throw new ArgumentNullException(nameof(candidate));
            }

            if (candidate.CandidateId == 0)
            {
                _context.Candidates.Add(candidate);
            }
            else
            {
                _context.Entry(candidate).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            return candidate;
        }
    }
}
