using Microsoft.AspNetCore.Mvc;
using OrderAndInventory.BLL.DTOs.Member.Requests;
using OrderAndInventory.BLL.DTOs.Member.Responses;
using OrderAndInventory.BLL.Services.Interfaces;
using Shared.DTOs;

namespace OrderAndInventory.API.Controllers;

[Route("api/members")]
public class MembersController : BaseApiController
{
    private readonly IMemberService _memberService;
    
    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(MemberDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync(CreateMemberRequest request, CancellationToken cancellationToken)
    {
        return (await _memberService.CreateAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginationResult<MemberDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromQuery] GetMembersRequest request, CancellationToken cancellationToken)
    {
        return (await _memberService.GetAsync(request, cancellationToken)).ToApiResponse();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MemberDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return (await _memberService.GetByIdAsync(id, cancellationToken)).ToApiResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(MemberDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(Guid id, UpdateMemberRequest request, CancellationToken cancellationToken)
    {
        return (await _memberService.UpdateAsync(id, request, cancellationToken)).ToApiResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return (await _memberService.DeleteAsync(id, cancellationToken)).ToApiResponse();
    }
}