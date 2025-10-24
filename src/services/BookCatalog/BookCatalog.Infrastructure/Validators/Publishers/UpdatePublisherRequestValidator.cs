using BookCatalog.Application.UseCases.Commands.Publishers.Update;
using FluentValidation;

namespace BookCatalog.Infrastructure.Validators.Publishers;

public class UpdatePublisherRequestValidator : AbstractValidator<UpdatePublisherCommand>
{
    public UpdatePublisherRequestValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Request.Address)
            .NotEmpty()
            .WithMessage("Address is required");
    }
}