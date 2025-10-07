using OrderAndInventory.BLL.DTOs.Order.Responses;

namespace OrderAndInventory.BLL.DTOs.Member.Responses;

public record MemberDto(
        Guid MemberId,
        string Email,
        string FirstName,
        string LastName,
        string PhoneNumber,
        OrderDto[] Orders);