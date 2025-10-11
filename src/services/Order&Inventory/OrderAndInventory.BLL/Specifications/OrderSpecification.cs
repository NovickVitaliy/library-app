using Ardalis.Specification;
using OrderAndInventory.BLL.DTOs.Order.Requests;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.Specifications;

public class OrderSpecification : Specification<Order>
{
    public OrderSpecification(GetOrdersRequest request, bool ignorePagination = false)
    {
        if (request.MinOrderDate.HasValue)
            Query.Where(o => o.OrderDate >= request.MinOrderDate.Value);

        if (request.MaxOrderDate.HasValue)
            Query.Where(o => o.OrderDate <= request.MaxOrderDate.Value);

        if (request.Status.HasValue)
            Query.Where(o => o.Status == request.Status.Value);

        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            switch (request.SortBy.ToLower())
            {
                case "orderdate":
                    if (request.SortDescending) Query.OrderByDescending(o => o.OrderDate);
                    else Query.OrderBy(o => o.OrderDate);
                    break;

                case "status":
                    if (request.SortDescending) Query.OrderByDescending(o => o.Status);
                    else Query.OrderBy(o => o.Status);
                    break;

                case "memberid":
                    if (request.SortDescending) Query.OrderByDescending(o => o.MemberId);
                    else Query.OrderBy(o => o.MemberId);
                    break;

                default:
                    if (request.SortDescending) Query.OrderByDescending(o => o.OrderId);
                    else Query.OrderBy(o => o.OrderId);
                    break;
            }
        }
        else
        {
            Query.OrderBy(o => o.OrderId);
        }

        if (!ignorePagination)
        {
            var skip = (request.PageNumber - 1) * request.PageSize;
            Query.Skip(skip).Take(request.PageSize);
        }
        
        Query.Include(o => o.Member)
            .Include(o => o.OrderItems)
            .Include(o => o.Payments)
            .Include(o => o.StaffOrders)
            .ThenInclude(x => x.Staff);
    }
}