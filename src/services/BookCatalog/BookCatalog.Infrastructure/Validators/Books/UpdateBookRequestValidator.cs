using BookCatalog.Application.UseCases.Commands.Books.Update;
using BookCatalog.Domain.Models;
using BookCatalog.Infrastructure.Database;
using FluentValidation;
using MongoDB.Driver;

namespace BookCatalog.Infrastructure.Validators.Books;

public class UpdateBookRequestValidator : AbstractValidator<UpdateBookCommand>
{
    private readonly BookCatalogDbContext _dbContext;

    public UpdateBookRequestValidator(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(x => x.BookId)
            .NotEmpty()
            .WithMessage("Book ID is required");
        
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
        
        RuleFor(x => x.Request.GenresIds)
            .MustAsync(AllGenresExistAsync)
            .WithMessage("Some of the given genres does not exist");
        
        
        RuleFor(x => x.Request.PublishersIds)
            .MustAsync(AllPublishersExistAsync)
            .WithMessage("Some of the given publishers does not exist");
    }

    private async Task<bool> AllPublishersExistAsync(Guid[] ids, CancellationToken cancellationToken)
    {
        var count = await _dbContext.Publishers.CountDocumentsAsync(
            Builders<Publisher>.Filter.In(p => p.PublisherId, ids),
            cancellationToken: cancellationToken);

        return count == ids.Length;
    }

    private async Task<bool> AllGenresExistAsync(Guid[] ids, CancellationToken cancellationToken)
    {
        var count = await _dbContext.Genres.CountDocumentsAsync(
            Builders<Genre>.Filter.In(g => g.GenreId, ids),
            cancellationToken: cancellationToken);

        return count == ids.Length;
    }
    
    private static bool BeEitherPhysicalOrDigital(UpdateBookCommand request)
    {
        var isPhysical = request.Request.Weight.HasValue || !string.IsNullOrWhiteSpace(request.Request.ShelfLocation);
        var isDigital = !string.IsNullOrWhiteSpace(request.Request.FileFormat) || !string.IsNullOrWhiteSpace(request.Request.DownloadLink);

        return isPhysical ^ isDigital;
    }

    private static bool IsPhysicalBook(UpdateBookCommand request)
    {
        var isPhysical = request.Request.Weight.HasValue || !string.IsNullOrWhiteSpace(request.Request.ShelfLocation);
        var isDigital = !string.IsNullOrWhiteSpace(request.Request.FileFormat) || !string.IsNullOrWhiteSpace(request.Request.DownloadLink);
        return isPhysical && !isDigital;
    }

    private static bool IsDigitalBook(UpdateBookCommand request)
    {
        var isPhysical = request.Request.Weight.HasValue || !string.IsNullOrWhiteSpace(request.Request.ShelfLocation);
        var isDigital = !string.IsNullOrWhiteSpace(request.Request.FileFormat) || !string.IsNullOrWhiteSpace(request.Request.DownloadLink);
        return isDigital && !isPhysical;
    }
}