using BookCatalog.Application.DTOs.Genres.Requests;
using BookCatalog.Application.DTOs.Genres.Responses;
using BookCatalog.Application.UseCases.Commands.Genres.Create;
using BookCatalog.Application.UseCases.Commands.Genres.Delete;
using BookCatalog.Application.UseCases.Commands.Genres.Update;
using BookCatalog.Application.UseCases.Queries.Genres.Get;
using BookCatalog.Application.UseCases.Queries.Genres.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace BookCatalog.API.Controllers;

[Route("api/genres")]
public class GenresController : BaseApiController
{
    private readonly ISender _sender;
    
    public GenresController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(CreateGenreRequest request, CancellationToken cancellationToken) =>
        (await _sender.Send(new CreateGenreCommand(request), cancellationToken)).ToApiResponse();
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        (await _sender.Send(new GetGenreByIdQuery(id), cancellationToken)).ToApiResponse();
    
    [HttpGet]
    [ProducesResponseType(typeof(PaginationResult<GenreDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromQuery] PaginationRequest request, CancellationToken cancellationToken) =>
        (await _sender.Send(new GetGenresQuery(request), cancellationToken)).ToApiResponse();
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(Guid id, UpdateGenreRequest request, CancellationToken cancellationToken) =>
        (await _sender.Send(new UpdateGenreCommand(id, request), cancellationToken)).ToApiResponse();
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        (await _sender.Send(new DeleteGenreCommand(id), cancellationToken)).ToApiResponse();
}