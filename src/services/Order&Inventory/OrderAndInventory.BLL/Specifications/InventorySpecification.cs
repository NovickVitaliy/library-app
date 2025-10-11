using Ardalis.Specification;
using OrderAndInventory.BLL.DTOs.Inventory.Requests;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.Specifications;

public class InventorySpecification : Specification<Inventory>
{
    public InventorySpecification(GetInventoriesRequest request, bool ignorePagination = false)
    {
        if (request.MinStockQuantity.HasValue)
        {
            Query.Where(x => x.StockQuantity >= request.MinStockQuantity);
        }
        
        if (request.MaxStockQuantity.HasValue)
        {
            Query.Where(x => x.StockQuantity <= request.MaxStockQuantity);
        }
        
        if (request.MinReorderLevel.HasValue)
        {
            Query.Where(x => x.ReorderLevel >= request.MinReorderLevel);
        }
        
        if (request.MaxReorderLevel.HasValue)
        {
            Query.Where(x => x.ReorderLevel <= request.MaxReorderLevel);
        }

        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            switch (request.SortBy.ToLower())
            {
                case "inventoryid":
                    if (request.SortDescending)
                        Query.OrderByDescending(x => x.InventoryId);
                    else
                        Query.OrderBy(x => x.InventoryId);
                    break;

                case "bookid":
                    if (request.SortDescending)
                        Query.OrderByDescending(x => x.BookId);
                    else
                        Query.OrderBy(x => x.BookId);
                    break;

                case "stockquantity":
                    if (request.SortDescending)
                        Query.OrderByDescending(x => x.StockQuantity);
                    else
                        Query.OrderBy(x => x.StockQuantity);
                    break;

                case "reorderlevel":
                    if (request.SortDescending)
                        Query.OrderByDescending(x => x.ReorderLevel);
                    else
                        Query.OrderBy(x => x.ReorderLevel);
                    break;

                default:
                    if (request.SortDescending)
                        Query.OrderByDescending(x => x.InventoryId);
                    else
                        Query.OrderBy(x => x.InventoryId);
                    break;
            }
        }
        else
        {
            Query.OrderBy(x => x.InventoryId);
        }

        if (!ignorePagination)
        {
            Query
                .Skip((request.PageNumber - 1) * request.PageNumber)
                .Take(request.PageSize);
        }
    }
}