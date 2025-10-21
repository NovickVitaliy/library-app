using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Genres.Responses;
using BookCatalog.Application.Mappers;
using Shared.CQRS.Queries;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Genres.Get;

public sealed class GetGenresQueryHandler : IQueryHandler<GetGenresQuery, Result<PaginationResult<GenreDto>>>
{
    private readonly IGenreRepository _genreRepository;
    
    public GetGenresQueryHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
    
    public async Task<Result<PaginationResult<GenreDto>>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        var paginationResult = await _genreRepository.GetAsync(request.Request.PageNumber, request.Request.PageSize, cancellationToken);
        
        return Result<PaginationResult<GenreDto>>.Ok(paginationResult.ToPaginatedDtos(GenreMapper.ToDto));
    }
}