using BookCatalog.Application.DTOs.Publishers.Requests;
using BookCatalog.Application.DTOs.Publishers.Responses;
using BookCatalog.Application.UseCases.Commands.Publishers.Create;
using BookCatalog.Application.UseCases.Commands.Publishers.Delete;
using BookCatalog.Application.UseCases.Commands.Publishers.Update;
using BookCatalog.Application.UseCases.Queries.Publishers.Get;
using BookCatalog.Application.UseCases.Queries.Publishers.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace BookCatalog.API.Controllers;

[Route("api/publishers")]
public class PublishersController
{
    private readonly ISender _sender;
    
    public PublishersController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(PublisherDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(CreatePublisherRequest request, CancellationToken cancellationToken) =>
        (await _sender.Send(new CreatePublisherCommand(request), cancellationToken)).ToApiResponse();
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PublisherDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        (await _sender.Send(new GetPublisherByIdQuery(id), cancellationToken)).ToApiResponse();
    
    [HttpGet]
    [ProducesResponseType(typeof(PaginationResult<PublisherDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromQuery] PaginationRequest request, CancellationToken cancellationToken) =>
        (await _sender.Send(new GetPublishersQuery(request), cancellationToken)).ToApiResponse();
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PublisherDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(Guid id, UpdatePublisherRequest request, CancellationToken cancellationToken) =>
        (await _sender.Send(new UpdatePublisherCommand(id, request), cancellationToken)).ToApiResponse();
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        (await _sender.Send(new DeletePublisherCommand(id), cancellationToken)).ToApiResponse();
}