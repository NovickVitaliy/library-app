using BookCatalog.Application.DTOs.Reviews.Requests;
using FluentValidation;

namespace BookCatalog.Application.Validators.Reviews;

public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
{
    public CreateReviewRequestValidator()
    {
        RuleFor(x => x.BookId)
            .MustAsync(BookMustExistAsync)
            .WithMessage("Book with such ID does not exist");

        RuleFor(x => x.UserId)
            .MustAsync(UserMustExistAsync)
            .WithMessage("User with such ID does not exist");

        RuleFor(x => x.Rating)
            .InclusiveBetween(0, 10)
            .WithMessage("Rating must be between 0 and 10");

        RuleFor(x => x.Text)
            .MaximumLength(200)
            .WithMessage("Maximum lenght of review test is 200 characters");
    }
    private Task<bool> UserMustExistAsync(Guid arg1, CancellationToken arg2)
    {
        // mock
        return Task.FromResult(true);
    }

    private Task<bool> BookMustExistAsync(Guid arg1, CancellationToken arg2)
    {
        // mock
        return Task.FromResult(true);
    }
}