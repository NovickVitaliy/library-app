using FluentValidation;
using OrderAndInventory.BLL.DTOs.OrderItem.Requests;

namespace OrderAndInventory.BLL.Validators.OrderItem;

public class CreateOrderItemRequestValidator : AbstractValidator<CreateOrderItemRequest>
{
    public CreateOrderItemRequestValidator()
    {
        RuleFor(x => x.BookId)
            .MustAsync(CheckIfBookExistsAsync)
            .WithMessage("Book with given id was not found");
    }
    
    private Task<bool> CheckIfBookExistsAsync(Guid bookId, CancellationToken cancellationToken)
    {
        // will be implemented in the future
        return Task.FromResult(true);
    }
}