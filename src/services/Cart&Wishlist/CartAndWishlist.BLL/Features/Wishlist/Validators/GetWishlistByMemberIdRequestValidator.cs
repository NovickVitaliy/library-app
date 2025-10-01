using CartAndWishlist.BLL.Features.Wishlist.DTOs.Requests;
using FluentValidation;

namespace CartAndWishlist.BLL.Features.Wishlist.Validators;

public class GetWishlistByMemberIdRequestValidator : AbstractValidator<GetWishlistByMemberIdRequest>
{
    public GetWishlistByMemberIdRequestValidator()
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