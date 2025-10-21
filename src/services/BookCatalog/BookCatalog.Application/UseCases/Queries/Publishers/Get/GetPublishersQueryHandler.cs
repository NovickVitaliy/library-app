using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Publishers.Responses;
using BookCatalog.Application.Mappers;
using Shared.CQRS.Queries;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Publishers.Get;

public sealed class GetPublishersQueryHandler : IQueryHandler<GetPublishersQuery, Result<PaginationResult<PublisherDto>>>
{
    private readonly IPublisherRepository _publisherRepository;
    
    public GetPublishersQueryHandler(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }
    
    public async Task<Result<PaginationResult<PublisherDto>>> Handle(GetPublishersQuery request, CancellationToken cancellationToken)
    {
        var paginationResult = await _publisherRepository.GetAsync(request.Request.PageNumber, request.Request.PageSize, cancellationToken);
        
        return Result<PaginationResult<PublisherDto>>.Ok(paginationResult.ToPaginatedDtos(PublisherMapper.ToDto));
    }
}