using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Books.Responses;
using BookCatalog.Application.Mappers;
using BookCatalog.Domain.Models;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Books.Create;

public sealed class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, Result<BookDto>>
{
    private readonly IBookRepository _bookRepository;
    
    public CreateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public async Task<Result<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book()
        {
            BookId = Guid.CreateVersion7(),
            Title = request.Request.Title,
            Author = request.Request.Author,
            DownloadLink = request.Request.DownloadLink,
            Edition = request.Request.Edition,
            FileFormat = request.Request.FileFormat,
            Illustrator = request.Request.Illustrator,
            Pages = request.Request.Pages,
            Price = request.Request.Price,
            ShelfLocation = request.Request.ShelfLocation,
            ShippingCost = request.Request.ShippingCost,
            Weight = request.Request.Weight,
            ReviewsIds = [],
            GenresIds = request.Request.GenresIds.ToList(),
            PublishersIds = request.Request.PublishersIds.ToList()
        };

        book = await _bookRepository.CreateAsync(book, cancellationToken);
        
        return Result<BookDto>.Created($"/api/books/{book.BookId}", BookMapper.ToDto(book));
    }
}