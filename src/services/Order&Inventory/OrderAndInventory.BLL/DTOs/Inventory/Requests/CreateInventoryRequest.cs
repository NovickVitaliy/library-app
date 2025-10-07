using System.ComponentModel.DataAnnotations;

namespace OrderAndInventory.BLL.DTOs.Inventory.Requests;

public record CreateInventoryRequest(
        [Required] Guid BookId,
        [Required] [Range(1, int.MaxValue)] int StockQuantity,
        [Required] [Range(1, int.MaxValue)] int ReorderLevel);