using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Genres.Responses;
using BookCatalog.Application.Mappers;
using BookCatalog.Domain.Models;
using Shared.CQRS.Queries;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Genres.GetById;

public sealed class GetGenreByIdQueryHandler : IQueryHandler<GetGenreByIdQuery, Result<GenreDto>>
{
    private readonly IGenreRepository _genreRepository;
    
    public GetGenreByIdQueryHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
    
    public async Task<Result<GenreDto>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.GetByIdAsync(request.GenreId, cancellationToken);

        return genre is null
            ? Result<GenreDto>.NotFound(key: request.GenreId, entityName: nameof(Genre))
            : Result<GenreDto>.Ok(GenreMapper.ToDto(genre));
    }
}