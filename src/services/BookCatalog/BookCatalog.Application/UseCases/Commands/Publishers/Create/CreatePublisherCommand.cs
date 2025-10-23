using BookCatalog.Application.DTOs.Publishers.Requests;
using BookCatalog.Application.DTOs.Publishers.Responses;
using Shared.CQRS.Commands;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Commands.Publishers.Create;

public record CreatePublisherCommand(CreatePublisherRequest Request) : ICommand<Result<PublisherDto>>;