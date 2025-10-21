using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Publishers.Responses;
using BookCatalog.Application.Mappers;
using BookCatalog.Domain.Models;
using Shared.CQRS.Queries;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Publishers.GetById;

public sealed class GetPublisherByIdQueryHandler : IQueryHandler<GetPublisherByIdQuery, Result<PublisherDto>>
{
    private readonly IPublisherRepository _publisherRepository;
    
    public GetPublisherByIdQueryHandler(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }
    
    public async Task<Result<PublisherDto>> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken)
    {
        var publisher = await _publisherRepository.GetByIdAsync(request.PublisherId, cancellationToken);

        return publisher is null
            ? Result<PublisherDto>.NotFound(key: request.PublisherId, entityName: nameof(Publisher))
            : Result<PublisherDto>.Ok(PublisherMapper.ToDto(publisher));
    }
}