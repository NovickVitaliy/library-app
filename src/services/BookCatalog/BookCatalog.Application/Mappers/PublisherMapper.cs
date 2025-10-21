using BookCatalog.Application.DTOs.Publishers.Responses;
using BookCatalog.Domain.Models;

namespace BookCatalog.Application.Mappers;

public static class PublisherMapper
{
    public static PublisherDto ToDto(Publisher publisher)
        => new PublisherDto(publisher.PublisherId, publisher.Name, publisher.Address.Value);
}