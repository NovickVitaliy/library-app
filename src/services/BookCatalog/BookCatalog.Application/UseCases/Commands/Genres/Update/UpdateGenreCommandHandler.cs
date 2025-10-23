using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Application.DTOs.Genres.Responses;
using BookCatalog.Application.Mappers;
using BookCatalog.Domain.Models;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Genres.Update;

public sealed class UpdateGenreCommandHandler : ICommandHandler<UpdateGenreCommand, Result<GenreDto>>
{
    private readonly IGenreRepository _genreRepository;
    
    public UpdateGenreCommandHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
    
    public async Task<Result<GenreDto>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.GetByIdAsync(request.GenreId, cancellationToken);

        if (genre is null)
        {
            return Result<GenreDto>.NotFound(key: request.GenreId, entityName: nameof(Genre));
        }

        genre.Description = request.Request.Description;
        genre.Name = request.Request.Name;

        await _genreRepository.UpdateAsync(request.GenreId, genre, cancellationToken);
        
        return Result<GenreDto>.Ok(GenreMapper.ToDto(genre));
    }
}