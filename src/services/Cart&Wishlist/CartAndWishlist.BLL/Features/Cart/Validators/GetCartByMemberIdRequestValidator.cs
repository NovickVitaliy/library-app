using CartAndWishlist.BLL.Features.Cart.DTOs.Requests;
using FluentValidation;

namespace CartAndWishlist.BLL.Features.Cart.Validators;

public class GetCartByMemberIdRequestValidator : AbstractValidator<GetCartByMemberIdRequest>
{
    public GetCartByMemberIdRequestValidator()
    {
        RuleFor(x => x.MemberId)
            .MustAsync(CheckIfMemberExistsAsync)
            .WithMessage("Member with such id does not exist");
    }
    
    private Task<bool> CheckIfMemberExistsAsync(Guid memberId, CancellationToken cancellationToken)
    {
        // we dont have interservice communication for now but it will be implemented in the future
        return Task.FromResult(true);
    }
}