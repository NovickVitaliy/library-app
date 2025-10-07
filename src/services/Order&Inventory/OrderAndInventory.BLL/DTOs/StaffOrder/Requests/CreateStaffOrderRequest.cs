using System.ComponentModel.DataAnnotations;

namespace OrderAndInventory.BLL.DTOs.StaffOrder.Requests;

public record CreateStaffOrderRequest(
    [Required(ErrorMessage = "Staff Id is required")] 
    Guid StaffId);