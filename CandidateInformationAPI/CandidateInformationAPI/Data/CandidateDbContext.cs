using Microsoft.EntityFrameworkCore;
using CandidateInformationAPI.Models;
using CandidateInformationAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CandidateInformationAPI.Data
{
    public class CandidateDbContext  : IdentityDbContext<ApplicationUser>
    {
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
    }
}
