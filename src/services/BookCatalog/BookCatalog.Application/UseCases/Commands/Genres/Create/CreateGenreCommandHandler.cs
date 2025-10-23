using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Genres.Responses;
using BookCatalog.Application.Mappers;
using BookCatalog.Domain.Models;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Genres.Create;

public sealed class CreateGenreCommandHandler : ICommandHandler<CreateGenreCommand, Result<GenreDto>>
{
    private readonly IGenreRepository _genreRepository;
    
    public CreateGenreCommandHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
    
    public async Task<Result<GenreDto>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = new Genre()
        {
            GenreId = Guid.CreateVersion7(),
            Description = request.Request.Description,
            Name = request.Request.Name
        };

        genre = await _genreRepository.CreateAsync(genre, cancellationToken);
        
        return Result<GenreDto>.Created($"/api/genres/{genre.GenreId}", GenreMapper.ToDto(genre));
    }
}