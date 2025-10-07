using Ardalis.Specification;
using OrderAndInventory.BLL.DTOs.Staff.Requests;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.Specifications;

public class StaffSpecification : Specification<Staff>
{
    public StaffSpecification(GetStavesRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.Name))
            Query.Where(s => s.Name.Contains(request.Name));

        if (!string.IsNullOrWhiteSpace(request.Role))
            Query.Where(s => s.Role.Contains(request.Role));

        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            switch (request.SortBy.ToLower())
            {
                case "name":
                    if (request.SortDescending) Query.OrderByDescending(s => s.Name);
                    else Query.OrderBy(s => s.Name);
                    break;

                case "role":
                    if (request.SortDescending) Query.OrderByDescending(s => s.Role);
                    else Query.OrderBy(s => s.Role);
                    break;

                default:
                    if (request.SortDescending) Query.OrderByDescending(s => s.StaffId);
                    else Query.OrderBy(s => s.StaffId);
                    break;
            }
        }
        else
        {
            Query.OrderBy(s => s.StaffId);
        }

        var skip = (request.PageNumber - 1) * request.PageSize;
        Query.Skip(skip).Take(request.PageSize);
    }
}