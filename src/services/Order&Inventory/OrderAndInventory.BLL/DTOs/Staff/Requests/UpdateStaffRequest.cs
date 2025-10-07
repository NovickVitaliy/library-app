using System.ComponentModel.DataAnnotations;

namespace OrderAndInventory.BLL.DTOs.Staff.Requests;

public record UpdateStaffRequest(
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(50, ErrorMessage = "Name must be less than 50 characters long")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
    string Name,
    [Required(ErrorMessage = "Role is required")]
    [MaxLength(50, ErrorMessage = "Role must be less than 50 characters long")]
    [MinLength(2, ErrorMessage = "Role must be at least 2 characters long")]
    string Role);