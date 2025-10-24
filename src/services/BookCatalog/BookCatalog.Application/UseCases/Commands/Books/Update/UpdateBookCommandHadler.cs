using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Books.Responses;
using BookCatalog.Application.Mappers;
using BookCatalog.Domain.Models;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Books.Update;

public sealed class UpdateBookCommandHadler : ICommandHandler<UpdateBookCommand, Result<BookDto>>
{
    private readonly IBookRepository _bookRepository;
    
    public UpdateBookCommandHadler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public async Task<Result<BookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);

        if (book is null)
        {
            return Result<BookDto>.NotFound(key:request.BookId, entityName: nameof(Book));
        }

        book.Title = request.Request.Title;
        book.Author = request.Request.Author;
        book.Pages = request.Request.Pages;
        book.ShelfLocation = request.Request.ShelfLocation;
        book.Price = request.Request.Price;
        book.Weight = request.Request.Weight;
        book.ShippingCost = request.Request.ShippingCost;
        book.FileFormat = request.Request.FileFormat;
        book.DownloadLink = request.Request.DownloadLink;
        book.Illustrator = request.Request.Illustrator;
        book.Edition = request.Request.Edition;
        book.GenresIds = request.Request.GenresIds.ToList();
        book.PublishersIds = request.Request.PublishersIds.ToList();

        book = await _bookRepository.UpdateAsync(request.BookId, book, cancellationToken);
        
        return Result<BookDto>.Ok(BookMapper.ToDto(book!));
    }
}