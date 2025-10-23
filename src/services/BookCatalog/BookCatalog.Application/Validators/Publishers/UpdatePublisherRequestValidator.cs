using BookCatalog.Application.DTOs.Publishers.Requests;
using FluentValidation;

namespace BookCatalog.Application.Validators.Publishers;

public class UpdatePublisherRequestValidator : AbstractValidator<UpdatePublisherRequest>
{
    public UpdatePublisherRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address is required");
    }
}