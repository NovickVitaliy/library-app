using OrderAndInventory.BLL.DTOs.Common;
using Shared.DTOs;

namespace OrderAndInventory.BLL.DTOs.Member.Requests;

public record GetMembersRequest(
    string? Email,
    string? FirstName,
    string? LastName,
    string? PhoneNumber) : GetRequest;