using System.ComponentModel.DataAnnotations;

namespace CandidateInformationAPI.DTOs
{
    public class RegisterUserDto
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}
