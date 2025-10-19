using BookCatalog.Domain.Models;
using Shared.DTOs;

namespace BookCatalog.Application.Contracts.Repositories;

public interface IBookRepository
{
    Task<Book> CreateAsync(Book book, CancellationToken cancellationToken);
    Task<Book?> GetByIdAsync(Guid bookId, CancellationToken cancellationToken);
    Task<PaginationResult<Book>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<long> CountAsync(CancellationToken cancellationToken);
    Task<Book?> UpdateAsync(Guid bookId, Book book, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid bookId, CancellationToken cancellationToken);
}