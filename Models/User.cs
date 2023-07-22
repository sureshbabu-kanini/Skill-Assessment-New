using SkillAssessment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public int User_ID { get; set; }

    [StringLength(50)]
    public string? User_FirstName { get; set; }

    [StringLength(50)]
    public string? User_LastName { get; set; }

    [StringLength(100)]
    public string? User_Departmenr { get; set; }

    [StringLength(100)]
    public string? User_Designation { get; set; }

    [DataType(DataType.Date)]
    public DateTime? User_DOB { get; set; }

    [StringLength(10)]
    public string? User_Gender { get; set; }

    [StringLength(100)]
    public string? User_EduLevel { get; set; }

    public long? User_PhoneNo { get; set; }

    [StringLength(100)]
    public string? User_Location { get; set; }

    [StringLength(200)]
    public string? User_Address { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? User_Email { get; set; }

    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
     ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter and one special character.")]
    public string? User_Password { get; set; }
    public string User_Image { get; set; }
    public ICollection<Assessment> assessments { get; set; }
    public ICollection<Result> results { get; set; }
}
