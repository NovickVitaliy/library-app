using BookCatalog.Application.DTOs.Genres.Requests;
using BookCatalog.Application.UseCases.Commands.Genres.Update;
using FluentValidation;

namespace BookCatalog.Application.Validators.Genres;

public class UpdateGenreRequestValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreRequestValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Request.Description)
            .NotEmpty()
            .WithMessage("Description is required");
    }
}