namespace OrderAndInventory.DAL.Models;

public class Inventory
{
    public Guid InventoryId { get; set; }
    public Guid BookId { get; set; }
    public int StockQuantity { get; set; }
    public int ReorderLevel { get; set; } // when the total quantity of item is below this number -> publish an event that item needs to be restocked
}