using BookCatalog.Application.UseCases.Commands.Publishers.Create;
using FluentValidation;

namespace BookCatalog.Infrastructure.Validators.Publishers;

public class CreatePublisherRequestValidator : AbstractValidator<CreatePublisherCommand>
{
    public CreatePublisherRequestValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Request.Address)
            .NotEmpty()
            .WithMessage("Address is required");
    }
}