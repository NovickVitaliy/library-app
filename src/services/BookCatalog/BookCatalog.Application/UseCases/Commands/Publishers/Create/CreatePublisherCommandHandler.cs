using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Publishers.Responses;
using BookCatalog.Application.Mappers;
using BookCatalog.Domain.Models;
using BookCatalog.Domain.ValueObjects;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Publishers.Create;

public sealed class CreatePublisherCommandHandler : ICommandHandler<CreatePublisherCommand, Result<PublisherDto>>
{
    private readonly IPublisherRepository _publisherRepository;
    
    public CreatePublisherCommandHandler(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }
    
    public async Task<Result<PublisherDto>> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = new Publisher()
        {
            Name = request.Request.Name,
            PublisherId = Guid.CreateVersion7(),
            Address = Address.Create(request.Request.Address)
        };

        await _publisherRepository.CreateAsync(publisher, cancellationToken);
        
        return Result<PublisherDto>.Created($"/api/publishers/{publisher.PublisherId}", PublisherMapper.ToDto(publisher));
    }
}