using BookCatalog.Application.DTOs.Publishers.Requests;
using BookCatalog.Application.DTOs.Publishers.Responses;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Publishers.Update;

public record UpdatePublisherCommand(Guid PublisherId, UpdatePublisherRequest Request) : ICommand<Result<PublisherDto>>;