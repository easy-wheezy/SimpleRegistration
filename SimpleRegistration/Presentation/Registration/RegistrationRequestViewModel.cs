using System.ComponentModel.DataAnnotations;

namespace SimpleRegistration.Presentation.Registration
{
    public class RegistrationRequestViewModel
    {
        [EmailAddress(ErrorMessage = "Invalid email entered")]
        [Required(ErrorMessage = "Yeah, you will need one of those electronic email things")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Yeah, you will need one of those password things")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Must contain at least, 1 upper case character, 1 lower case character and a number")]
        public string Password { get; set; }
    }
}
