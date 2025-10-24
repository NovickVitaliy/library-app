using BookCatalog.Application.DTOs.Reviews.Requests;
using BookCatalog.Application.DTOs.Reviews.Responses;
using BookCatalog.Application.UseCases.Commands.Reviews.Create;
using BookCatalog.Application.UseCases.Commands.Reviews.Delete;
using BookCatalog.Application.UseCases.Commands.Reviews.Update;
using BookCatalog.Application.UseCases.Queries.Reviews.Get;
using BookCatalog.Application.UseCases.Queries.Reviews.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.API.Controllers;

[Route("api/reviews")]
public class ReviewsController
{
    private readonly ISender _sender;
    
    public ReviewsController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(CreateReviewRequest request, CancellationToken cancellationToken) =>
        (await _sender.Send(new CreateReviewCommand(request), cancellationToken)).ToApiResponse();
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        (await _sender.Send(new GetReviewByIdQuery(id), cancellationToken)).ToApiResponse();
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(Guid id, UpdateReviewRequest request, CancellationToken cancellationToken) =>
        (await _sender.Send(new UpdateReviewCommand(id, request), cancellationToken)).ToApiResponse();
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        (await _sender.Send(new DeleteReviewCommand(id), cancellationToken)).ToApiResponse();
}