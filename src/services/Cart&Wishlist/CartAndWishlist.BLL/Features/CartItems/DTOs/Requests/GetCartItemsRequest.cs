using System.ComponentModel.DataAnnotations;
using Shared.DTOs;

namespace CartAndWishlist.BLL.Features.CartItems.DTOs.Requests;

public record GetCartItemsRequest : PaginationRequest;