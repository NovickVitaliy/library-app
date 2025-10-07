using FluentValidation;
using OrderAndInventory.BLL.DTOs.Inventory.Requests;

namespace OrderAndInventory.BLL.Validators.Inventory;

public class CreateInventoryRequestValidator : AbstractValidator<CreateInventoryRequest>
{
    public CreateInventoryRequestValidator()
    {
        RuleFor(x => x.BookId)
            .MustAsync(CheckIfBookExistsAsync)
            .WithMessage("Book with given Id was not found");
    }
    
    private Task<bool> CheckIfBookExistsAsync(Guid bookId, CancellationToken cancellationToken)
    {
        // will be implemented in the future
        return Task.FromResult(true);
    }
}