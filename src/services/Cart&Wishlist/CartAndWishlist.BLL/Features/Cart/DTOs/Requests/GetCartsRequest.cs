using System.ComponentModel.DataAnnotations;
using Shared.DTOs;

namespace CartAndWishlist.BLL.Features.Cart.DTOs.Requests;

public record GetCartsRequest : PaginationRequest;