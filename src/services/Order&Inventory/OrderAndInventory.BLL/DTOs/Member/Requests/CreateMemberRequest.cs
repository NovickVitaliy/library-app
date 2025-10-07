using System.ComponentModel.DataAnnotations;

namespace OrderAndInventory.BLL.DTOs.Member.Requests;

public record CreateMemberRequest(
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email must be in a valid format")]
    string Email,
    [Required(ErrorMessage = "First Name is required")]
    [MaxLength(50, ErrorMessage = "First Name must be less than 50 characters long")]
    [MinLength(2, ErrorMessage = "First Name must be at least 2 characters long")]
    string FirstName,
    [Required(ErrorMessage = "Last Name is required")]
    [MaxLength(50, ErrorMessage = "Last Name must be less than 50 characters long")]
    [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters long")]
    string LastName,
    [Required(ErrorMessage = "Phone Number is required")]
    string PhoneNumber);