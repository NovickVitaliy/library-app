using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Publishers.Delete;

public record DeletePublisherCommand(Guid PublisherId) : ICommand<Result<bool>>;