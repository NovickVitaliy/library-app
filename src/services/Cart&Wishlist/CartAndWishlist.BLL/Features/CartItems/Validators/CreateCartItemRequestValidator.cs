using CartAndWishlist.BLL.Features.CartItems.DTOs.Requests;
using FluentValidation;

namespace CartAndWishlist.BLL.Features.CartItems.Validators;

public class CreateCartItemRequestValidator : AbstractValidator<CreateCartItemRequest>
{
    public CreateCartItemRequestValidator()
    {
        RuleFor(x => x.BookId)
            .MustAsync(CheckIfBookExistsAsync)
            .WithMessage("Book with such id does not exist");
    }
    
    private Task<bool> CheckIfBookExistsAsync(Guid bookId, CancellationToken cancellationToken)
    {
        // we dont have interservice communication for now but it will be implemented in the future
        return Task.FromResult(true);
    }
}