using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Publishers.Responses;
using BookCatalog.Application.Mappers;
using BookCatalog.Domain.Models;
using BookCatalog.Domain.ValueObjects;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Publishers.Update;

public sealed class UpdatePublisherCommandHandler : ICommandHandler<UpdatePublisherCommand, Result<PublisherDto>>
{
    private readonly IPublisherRepository _publisherRepository;
    
    public UpdatePublisherCommandHandler(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }
    
    public async Task<Result<PublisherDto>> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _publisherRepository.GetByIdAsync(request.PublisherId, cancellationToken);
        if (publisher is null)
        {
            return Result<PublisherDto>.NotFound(key: request.PublisherId, entityName: nameof(Publisher));
        }

        publisher.Name = request.Request.Name;
        publisher.Address = Address.Create(request.Request.Address);

        publisher = await _publisherRepository.UpdateAsync(request.PublisherId, publisher, cancellationToken);
        
        return Result<PublisherDto>.Ok(PublisherMapper.ToDto(publisher!));
    }
}