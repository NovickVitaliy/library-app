using BookCatalog.Application.DTOs.Publishers.Responses;
using Shared.CQRS.Queries;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace BookCatalog.Application.UseCases.Queries.Publishers.Get;

public record GetPublishersQuery(PaginationRequest Request) : IQuery<Result<PaginationResult<PublisherDto>>>;