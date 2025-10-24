using BookCatalog.Application.UseCases.Commands.Reviews.Create;
using BookCatalog.Domain.Models;
using BookCatalog.Infrastructure.Database;
using FluentValidation;
using MongoDB.Driver;

namespace BookCatalog.Infrastructure.Validators.Reviews;

public class CreateReviewRequestValidator : AbstractValidator<CreateReviewCommand>
{
    private readonly BookCatalogDbContext _dbContext;
    
    public CreateReviewRequestValidator(BookCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(x => x.Request.BookId)
            .MustAsync(BookMustExistAsync)
            .WithMessage("Book with such ID does not exist");

        RuleFor(x => x.Request.UserId)
            .MustAsync(UserMustExistAsync)
            .WithMessage("User with such ID does not exist");

        RuleFor(x => x.Request.Rating)
            .InclusiveBetween(0, 10)
            .WithMessage("Rating must be between 0 and 10");

        RuleFor(x => x.Request.Text)
            .MaximumLength(200)
            .WithMessage("Maximum lenght of review test is 200 characters");
    }
    private Task<bool> UserMustExistAsync(Guid id, CancellationToken cancellationToken)
    {
        // mock (its gonna be a call to the users api in the future)
        return Task.FromResult(true);
    }

    private async Task<bool> BookMustExistAsync(Guid id, CancellationToken cancellationToken)
    {
        var count = await _dbContext.Books.CountDocumentsAsync(
            Builders<Book>.Filter.Eq(x => x.BookId, id),
            cancellationToken: cancellationToken);

        return count == 1;
    }
}