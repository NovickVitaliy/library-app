using BookCatalog.Application.DTOs.Books.Requests;
using FluentValidation;

namespace BookCatalog.Application.Validators.Books;

public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
{
    public CreateBookRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200);

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Author is required.")
            .MaximumLength(200);

        RuleFor(x => x.Pages)
            .GreaterThan(0).WithMessage("Pages must be greater than 0.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.ShippingCost)
            .GreaterThanOrEqualTo(0).WithMessage("Shipping cost cannot be negative.");

        RuleFor(x => x)
            .Must(BeEitherPhysicalOrDigital)
            .WithMessage("A book must be either physical or digital, not both.");

        When(IsPhysicalBook, () =>
        {
            RuleFor(x => x.Weight)
                .NotNull().WithMessage("Weight is required for physical books.")
                .GreaterThan(0).WithMessage("Weight must be greater than 0.");

            RuleFor(x => x.ShelfLocation)
                .NotEmpty().WithMessage("Shelf location is required for physical books.");
        });

        When(IsDigitalBook, () =>
        {
            RuleFor(x => x.FileFormat)
                .NotEmpty().WithMessage("File format is required for digital books.");

            RuleFor(x => x.DownloadLink)
                .NotEmpty().WithMessage("Download link is required for digital books.")
                .Must(link => Uri.IsWellFormedUriString(link, UriKind.Absolute))
                .WithMessage("Download link must be a valid URL.");
        });
    }

    private static bool BeEitherPhysicalOrDigital(CreateBookRequest request)
    {
        var isPhysical = request.Weight.HasValue || !string.IsNullOrWhiteSpace(request.ShelfLocation);
        var isDigital = !string.IsNullOrWhiteSpace(request.FileFormat) || !string.IsNullOrWhiteSpace(request.DownloadLink);

        return isPhysical ^ isDigital;
    }

    private static bool IsPhysicalBook(CreateBookRequest request)
    {
        var isPhysical = request.Weight.HasValue || !string.IsNullOrWhiteSpace(request.ShelfLocation);
        var isDigital = !string.IsNullOrWhiteSpace(request.FileFormat) || !string.IsNullOrWhiteSpace(request.DownloadLink);
        return isPhysical && !isDigital;
    }

    private static bool IsDigitalBook(CreateBookRequest request)
    {
        var isPhysical = request.Weight.HasValue || !string.IsNullOrWhiteSpace(request.ShelfLocation);
        var isDigital = !string.IsNullOrWhiteSpace(request.FileFormat) || !string.IsNullOrWhiteSpace(request.DownloadLink);
        return isDigital && !isPhysical;
    }
}