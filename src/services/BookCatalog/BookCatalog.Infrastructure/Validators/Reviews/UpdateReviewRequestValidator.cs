using BookCatalog.Application.DTOs.Reviews.Requests;
using FluentValidation;

namespace BookCatalog.Infrastructure.Validators.Reviews;

public class UpdateReviewRequestValidator : AbstractValidator<UpdateReviewRequest>
{
    public UpdateReviewRequestValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(0, 10)
            .WithMessage("Rating must be between 0 and 10");

        RuleFor(x => x.Text)
            .MaximumLength(200)
            .WithMessage("Maximum lenght of review test is 200 characters");
    }
}