using BookCatalog.Application.DTOs.Books.Requests;
using BookCatalog.Application.UseCases.Commands.Books.Create;
using FluentValidation;

namespace BookCatalog.Application.Validators.Books;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Request.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200);

        RuleFor(x => x.Request.Author)
            .NotEmpty().WithMessage("Author is required.")
            .MaximumLength(200);
        
        RuleFor(x => x.Request.Pages)
            .GreaterThan(0).WithMessage("Pages must be greater than 0.");
        
        RuleFor(x => x.Request.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
        
        RuleFor(x => x.Request.ShippingCost)
            .GreaterThanOrEqualTo(0).WithMessage("Shipping cost cannot be negative.");
        
        RuleFor(x => x)
            .Must(BeEitherPhysicalOrDigital)
            .WithMessage("A book must be either physical or digital, not both.");
        
        When(IsPhysicalBook, () =>
        {
            RuleFor(x => x.Request.Weight)
                .NotNull().WithMessage("Weight is required for physical books.")
                .GreaterThan(0).WithMessage("Weight must be greater than 0.");
        
            RuleFor(x => x.Request.ShelfLocation)
                .NotEmpty().WithMessage("Shelf location is required for physical books.");
        });
        
        When(IsDigitalBook, () =>
        {
            RuleFor(x => x.Request.FileFormat)
                .NotEmpty().WithMessage("File format is required for digital books.");
        
            RuleFor(x => x.Request.DownloadLink)
                .NotEmpty().WithMessage("Download link is required for digital books.")
                .Must(link => Uri.IsWellFormedUriString(link, UriKind.Absolute))
                .WithMessage("Download link must be a valid URL.");
        });
    }

    private static bool BeEitherPhysicalOrDigital(CreateBookCommand request)
    {
        var isPhysical = request.Request.Weight.HasValue || !string.IsNullOrWhiteSpace(request.Request.ShelfLocation);
        var isDigital = !string.IsNullOrWhiteSpace(request.Request.FileFormat) || !string.IsNullOrWhiteSpace(request.Request.DownloadLink);

        return isPhysical ^ isDigital;
    }

    private static bool IsPhysicalBook(CreateBookCommand request)
    {
        var isPhysical = request.Request.Weight.HasValue || !string.IsNullOrWhiteSpace(request.Request.ShelfLocation);
        var isDigital = !string.IsNullOrWhiteSpace(request.Request.FileFormat) || !string.IsNullOrWhiteSpace(request.Request.DownloadLink);
        return isPhysical && !isDigital;
    }

    private static bool IsDigitalBook(CreateBookCommand request)
    {
        var isPhysical = request.Request.Weight.HasValue || !string.IsNullOrWhiteSpace(request.Request.ShelfLocation);
        var isDigital = !string.IsNullOrWhiteSpace(request.Request.FileFormat) || !string.IsNullOrWhiteSpace(request.Request.DownloadLink);
        return isDigital && !isPhysical;
    }
}