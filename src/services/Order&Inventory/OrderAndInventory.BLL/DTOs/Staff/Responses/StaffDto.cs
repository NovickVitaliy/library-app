using OrderAndInventory.BLL.DTOs.Order.Responses;

namespace OrderAndInventory.BLL.DTOs.Staff.Responses;

public record StaffDto(
        Guid StaffId,
        string Name,
        string Role,
        OrderDto[] Orders);