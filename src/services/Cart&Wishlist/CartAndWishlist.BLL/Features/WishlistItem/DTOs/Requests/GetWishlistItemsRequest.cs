using System.ComponentModel.DataAnnotations;
using Shared.DTOs;

namespace CartAndWishlist.BLL.Features.WishlistItem.DTOs.Requests;

public record GetWishlistItemsRequest : PaginationRequest;