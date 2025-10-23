using BookCatalog.Application.Contracts.Repositories;
using BookCatalog.Domain.Models;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Publishers.Delete;

public sealed class DeletePublisherCommandHandler : ICommandHandler<DeletePublisherCommand, Result<bool>>
{
    private readonly IPublisherRepository _publisherRepository;
    
    public DeletePublisherCommandHandler(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }
    
    public async Task<Result<bool>> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _publisherRepository.GetByIdAsync(request.PublisherId, cancellationToken);

        if (publisher is null)
        {
            return Result<bool>.NotFound(key: request.PublisherId, entityName: nameof(Publisher));
        }

        var success = await _publisherRepository.DeleteAsync(request.PublisherId, cancellationToken);

        return success
            ? Result<bool>.NoContent()
            : Result<bool>.BadRequest("Error during deletion of publisher");
    }
}