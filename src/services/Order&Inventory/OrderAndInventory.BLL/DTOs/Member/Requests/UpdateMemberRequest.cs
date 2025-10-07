namespace OrderAndInventory.BLL.DTOs.Member.Requests;

public record UpdateMemberRequest(
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber);