using BookCatalog.Application.DTOs.Reviews.Responses;
using BookCatalog.Domain.Models;

namespace BookCatalog.Application.Mappers;

public static class ReviewMapper
{
    public static ReviewDto ToDto(Review review)
        => new ReviewDto(review.ReviewId, review.BookId, review.UserId, review.Rating, review.Text, review.CreatedDate);
}