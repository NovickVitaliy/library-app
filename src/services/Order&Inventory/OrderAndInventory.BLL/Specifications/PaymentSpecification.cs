using Ardalis.Specification;
using OrderAndInventory.BLL.DTOs.Payment.Requests;
using OrderAndInventory.DAL.Models;

namespace OrderAndInventory.BLL.Specifications;

public class PaymentSpecification : Specification<Payment>
{
    public PaymentSpecification(GetPaymentsRequest request, bool ignorePagination = false)
    {
        if (request.MinAmount.HasValue)
            Query.Where(p => p.Amount >= request.MinAmount.Value);

        if (request.MaxAmount.HasValue)
            Query.Where(p => p.Amount <= request.MaxAmount.Value);

        if (request.MinPaidDate.HasValue)
            Query.Where(p => p.PaidDate >= request.MinPaidDate.Value);

        if (request.MaxPaidDate.HasValue)
            Query.Where(p => p.PaidDate <= request.MaxPaidDate.Value);

        if (request.PaymentMethod.HasValue)
            Query.Where(p => p.PaymentMethod == request.PaymentMethod.Value);

        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            switch (request.SortBy.ToLower())
            {
                case "amount":
                    if (request.SortDescending) Query.OrderByDescending(p => p.Amount);
                    else Query.OrderBy(p => p.Amount);
                    break;

                case "paiddate":
                    if (request.SortDescending) Query.OrderByDescending(p => p.PaidDate);
                    else Query.OrderBy(p => p.PaidDate);
                    break;

                case "paymentmethod":
                    if (request.SortDescending) Query.OrderByDescending(p => p.PaymentMethod);
                    else Query.OrderBy(p => p.PaymentMethod);
                    break;

                case "orderid":
                    if (request.SortDescending) Query.OrderByDescending(p => p.OrderId);
                    else Query.OrderBy(p => p.OrderId);
                    break;

                default:
                    if (request.SortDescending) Query.OrderByDescending(p => p.PaymentId);
                    else Query.OrderBy(p => p.PaymentId);
                    break;
            }
        }
        else
        {
            Query.OrderBy(p => p.PaymentId);
        }

        if (!ignorePagination)
        {
            var skip = (request.PageNumber - 1) * request.PageSize;
            Query.Skip(skip).Take(request.PageSize);
        }
        
        Query.Include(p => p.Order);
    }
}