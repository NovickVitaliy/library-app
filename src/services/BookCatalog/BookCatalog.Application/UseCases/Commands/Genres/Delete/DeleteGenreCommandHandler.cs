using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Domain.Models;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Genres.Delete;

public sealed class DeleteGenreCommandHandler : ICommandHandler<DeleteGenreCommand, Result<bool>>
{
    private readonly IGenreRepository _genreRepository;
    
    public DeleteGenreCommandHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
    
    public async Task<Result<bool>> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.GetByIdAsync(request.GenreId, cancellationToken);
        if (genre is null)
        {
            return Result<bool>.NotFound(key: request.GenreId, entityName: nameof(Genre));
        }

        var success = await _genreRepository.DeleteAsync(request.GenreId, cancellationToken);

        return success
            ? Result<bool>.NoContent()
            : Result<bool>.BadRequest("Error during deletion of the genre");
    }
}