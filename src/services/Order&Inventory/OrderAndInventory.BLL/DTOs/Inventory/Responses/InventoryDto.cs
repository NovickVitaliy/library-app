namespace OrderAndInventory.BLL.DTOs.Inventory.Responses;

public record InventoryDto(
        Guid InventoryId,
        Guid BookId,
        int StockQuantity,
        int ReorderLevel);