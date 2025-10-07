using OrderAndInventory.BLL.DTOs.Common;
using Shared.DTOs;

namespace OrderAndInventory.BLL.DTOs.Staff.Requests;

public record GetStavesRequest(
    string? Name,
    string? Role) : GetRequest;