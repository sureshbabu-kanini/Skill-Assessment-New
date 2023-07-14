using System.ComponentModel.DataAnnotations;
using SkillAssessment.Models;

public class TokenUser
{
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string User_Email { get; set; }

    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one special character.")]
    public string User_Password { get; set; }
}
