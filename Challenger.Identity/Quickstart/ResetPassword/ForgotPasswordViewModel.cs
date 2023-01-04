using System.ComponentModel.DataAnnotations;

namespace Challenger.Identity.Quickstart.ResetPassword
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
