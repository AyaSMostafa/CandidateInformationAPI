using System.ComponentModel.DataAnnotations;

namespace CandidateInformationAPI.DTOs
{
    public class CandidateDto
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name must not exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name must not exceed 50 characters")]
        public string LastName { get; set; }

        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Invalid Egyption phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        public string CallTimeInterval { get; set; }

        [RegularExpression(@"^(https?:\/\/)?([\w]+\.)?linkedin\.com\/in\/[\w-]+$", ErrorMessage = "Invalid LinkedIn URL")]
        public string LinkedInUrl { get; set; }

        [RegularExpression(@"^(https?:\/\/)?(www\.)?github\.com\/[\w-]+(\/?[\w-]+)*\/?$", ErrorMessage = "Invalid GitHub URL")]
        public string GitHubUrl { get; set; }
        [Required(ErrorMessage = "Free Text Comment is required")]

        public string FreeTextComment { get; set; }
    }
}
