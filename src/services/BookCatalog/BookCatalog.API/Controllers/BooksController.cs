using BookCatalog.Application.DTOs.Books.Requests;
using BookCatalog.Application.DTOs.Books.Responses;
using BookCatalog.Application.DTOs.Reviews.Responses;
using BookCatalog.Application.UseCases.Commands.Books.Create;
using BookCatalog.Application.UseCases.Commands.Books.Delete;
using BookCatalog.Application.UseCases.Commands.Books.Update;
using BookCatalog.Application.UseCases.Queries.Books.Get;
using BookCatalog.Application.UseCases.Queries.Books.GetById;
using BookCatalog.Application.UseCases.Queries.Reviews.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace BookCatalog.API.Controllers;

[Route("api/books")]
public class BooksController : BaseApiController
{
    private readonly ISender _sender;
    
    public BooksController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(CreateBookRequest request, CancellationToken cancellationToken) =>
        (await _sender.Send(new CreateBookCommand(request), cancellationToken)).ToApiResponse();
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        (await _sender.Send(new GetBookByIdQuery(id), cancellationToken)).ToApiResponse();
    
    [HttpGet]
    [ProducesResponseType(typeof(PaginationResult<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromQuery] PaginationRequest request, CancellationToken cancellationToken) =>
        (await _sender.Send(new GetBooksQuery(request), cancellationToken)).ToApiResponse();
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(Guid id, UpdateBookRequest request, CancellationToken cancellationToken) =>
        (await _sender.Send(new UpdateBookCommand(id, request), cancellationToken)).ToApiResponse();
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        (await _sender.Send(new DeleteBookCommand(id), cancellationToken)).ToApiResponse();
    
    [HttpGet("{id:guid}/reviews")]
    [ProducesResponseType(typeof(PaginationResult<ReviewDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMovieReviews(Guid id, [FromQuery] PaginationRequest request, CancellationToken cancellationToken) =>
        (await _sender.Send(new GetReviewsQuery(id, request), cancellationToken)).ToApiResponse();
}