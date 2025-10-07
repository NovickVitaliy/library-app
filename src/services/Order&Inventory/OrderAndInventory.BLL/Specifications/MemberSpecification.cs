using Ardalis.Specification;
using OrderAndInventory.BLL.DTOs.Member.Requests;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.Specifications;

public class MemberSpecification : Specification<Member>
{
    public MemberSpecification(GetMembersRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            Query.Where(m => m.Email.Contains(request.Email));
        }

        if (!string.IsNullOrWhiteSpace(request.FirstName))
        {
            Query.Where(m => m.FirstName.Contains(request.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(request.LastName))
        {
            Query.Where(m => m.LastName.Contains(request.LastName));
        }

        if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            Query.Where(m => m.PhoneNumber.Contains(request.PhoneNumber));
        }

        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            switch (request.SortBy.ToLower())
            {
                case "email":
                    if (request.SortDescending) Query.OrderByDescending(m => m.Email);
                    else Query.OrderBy(m => m.Email);
                    break;

                case "firstname":
                    if (request.SortDescending) Query.OrderByDescending(m => m.FirstName);
                    else Query.OrderBy(m => m.FirstName);
                    break;

                case "lastname":
                    if (request.SortDescending) Query.OrderByDescending(m => m.LastName);
                    else Query.OrderBy(m => m.LastName);
                    break;

                case "phonenumber":
                    if (request.SortDescending) Query.OrderByDescending(m => m.PhoneNumber);
                    else Query.OrderBy(m => m.PhoneNumber);
                    break;

                default:
                    if (request.SortDescending) Query.OrderByDescending(m => m.MemberId);
                    else Query.OrderBy(m => m.MemberId);
                    break;
            }
        }
        else
        {
            Query.OrderBy(m => m.MemberId);
        }

        var skip = (request.PageNumber - 1) * request.PageSize;
        Query.Skip(skip).Take(request.PageSize);

        Query.Include(x => x.Orders)
            .ThenInclude(x => x.OrderItems)
            .Include(x => x.Orders)
            .ThenInclude(x => x.Payments)
            .Include(x => x.Orders)
            .ThenInclude(x => x.StaffOrders)
            .ThenInclude(x => x.Staff);
    }
}