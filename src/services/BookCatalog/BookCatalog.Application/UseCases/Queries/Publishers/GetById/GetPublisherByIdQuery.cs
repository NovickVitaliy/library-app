using BookCatalog.Application.DTOs.Publishers.Responses;
using Shared.CQRS.Queries;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Publishers.GetById;

public record GetPublisherByIdQuery(Guid PublisherId) : IQuery<Result<PublisherDto>>;