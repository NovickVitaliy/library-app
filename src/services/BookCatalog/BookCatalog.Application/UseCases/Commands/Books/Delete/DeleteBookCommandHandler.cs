using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Domain.Models;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Books.Delete;

public sealed class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand, Result<bool>>
{
    private readonly IBookRepository _bookRepository;
    
    public DeleteBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public async Task<Result<bool>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);

        if (book is null)
        {
            return Result<bool>.NotFound(key: request.BookId, entityName: nameof(Book));
        }

        var success = await _bookRepository.DeleteAsync(request.BookId, cancellationToken);

        return success
            ? Result<bool>.NoContent()
            : Result<bool>.BadRequest("Error during deletion of the book");
    }
}