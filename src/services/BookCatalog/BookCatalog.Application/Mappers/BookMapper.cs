using BookCatalog.Application.DTOs.Books.Responses;
using BookCatalog.Domain.Models;

namespace BookCatalog.Application.Mappers;

public static class BookMapper
{
    public static BookDto ToDto(Book book) => new BookDto(
            book.BookId,
            book.Title,
            book.Author,
            book.Pages,
            book.ShelfLocation,
            book.Price,
            book.Weight,
            book.ShippingCost,
            book.FileFormat,
            book.DownloadLink,
            book.Illustrator,
            book.Edition,
            book.Publishers.Select(PublisherMapper.ToDto).ToArray(),
            book.Genres.Select(GenreMapper.ToDto).ToArray(),
            book.Reviews.Select(ReviewMapper.ToDto).ToArray());
}