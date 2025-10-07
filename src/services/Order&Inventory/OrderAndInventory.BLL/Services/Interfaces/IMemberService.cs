using OrderAndInventory.BLL.DTOs.Member.Requests;
using OrderAndInventory.BLL.DTOs.Member.Responses;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace OrderAndInventory.BLL.Services.Interfaces;

public interface IMemberService
{
    Task<Result<MemberDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<PaginationResult<MemberDto>>> GetAsync(GetMembersRequest request, CancellationToken cancellationToken);
    Task<Result<MemberDto?>> CreateAsync(CreateMemberRequest request, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<MemberDto?>> UpdateAsync(Guid id, UpdateMemberRequest request, CancellationToken cancellationToken);
}