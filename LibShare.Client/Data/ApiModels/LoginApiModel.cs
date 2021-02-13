using System.ComponentModel.DataAnnotations;

namespace LibShare.Client.ApiModels
{
    public class LoginApiModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Password Code must be 5 characters")]
        public string Password { get; set; }

        public string RecaptchaToken { get; set; }
    }
}
