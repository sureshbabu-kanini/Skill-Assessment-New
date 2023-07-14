using SkillAssessment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public int User_ID { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50)]
    public string User_FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50)]
    public string User_LastName { get; set; }

    [Required(ErrorMessage = "Department is required.")]
    [StringLength(100)]
    public string User_Departmenr { get; set; }

    [Required(ErrorMessage = "Designation is required.")]
    [StringLength(100)]
    public string User_Designation { get; set; }

    [Required(ErrorMessage = "Date of birth is required.")]
    [DataType(DataType.Date)]
    public DateTime User_DOB { get; set; }

    [Required(ErrorMessage = "Gender is required.")]
    [StringLength(10)]
    public string User_Gender { get; set; }

    [Required(ErrorMessage = "Education level is required.")]
    [StringLength(100)]
    public string User_EduLevel { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    public long User_PhoneNo { get; set; }

    [Required(ErrorMessage = "Location is required.")]
    [StringLength(100)]
    public string User_Location { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(200)]
    public string User_Address { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string User_Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
     ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter and one special character.")]
    public string User_Password { get; set; }
    public ICollection<Assessment> assessments { get; set; }
}
