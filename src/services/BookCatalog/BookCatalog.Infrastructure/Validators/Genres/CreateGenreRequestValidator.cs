using BookCatalog.Application.UseCases.Commands.Genres.Create;
using FluentValidation;

namespace BookCatalog.Infrastructure.Validators.Genres;

public class CreateGenreRequestValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreRequestValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Request.Description)
            .NotEmpty()
            .WithMessage("Description is required");
    }
}