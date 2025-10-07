using Shared.DTOs;

namespace OrderAndInventory.BLL.DTOs.Common;

public record GetRequest(string? SortBy = "", bool SortDescending = false) : PaginationRequest;