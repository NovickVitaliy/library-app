using BookCatalog.Application.DTOs.Publishers.Requests;
using FluentValidation;

namespace BookCatalog.Application.Validators.Publishers;

public class CreatePublisherRequestValidator : AbstractValidator<CreatePublisherRequest>
{
    public CreatePublisherRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address is required");
    }
}