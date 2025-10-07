using OrderAndInventory.BLL.DTOs.Common;
using Shared.DTOs;

namespace OrderAndInventory.BLL.DTOs.Inventory.Requests;

public record GetInventoriesRequest(
    int? MinStockQuantity,
    int? MaxStockQuantity,
    int? MinReorderLevel,
    int? MaxReorderLevel) : GetRequest;