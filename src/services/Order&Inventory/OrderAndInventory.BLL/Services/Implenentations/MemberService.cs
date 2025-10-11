using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using OrderAndInventory.BLL.DTOs.Member.Requests;
using OrderAndInventory.BLL.DTOs.Member.Responses;
using OrderAndInventory.BLL.Services.Interfaces;
using OrderAndInventory.BLL.Specifications;
using OrderAndInventory.DAL.Models;
using OrderAndInventory.DAL.UOW.Interfaces;
using Shared.DTOs;
using Shared.ErrorHandling;

namespace OrderAndInventory.BLL.Services.Implenentations;

public class MemberService : IMemberService
{
    private readonly ILogger<MemberService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MemberService(
        ILogger<MemberService> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<MemberDto?>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching member with ID: {MemberId}", id);

        var member = await _unitOfWork.MemberRepository.GetByIdAsync(id, cancellationToken);

        if (member is null)
        {
            _logger.LogWarning("Member with ID {MemberId} not found", id);
            return Result<MemberDto?>.NotFound(key: id, entityName: nameof(Member));
        }

        _logger.LogInformation("Successfully retrieved member with ID: {MemberId}", id);
        return Result<MemberDto?>.Ok(_mapper.Map<MemberDto>(member));
    }

    public async Task<Result<PaginationResult<MemberDto>>> GetAsync(GetMembersRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching members with filters: {@Request}", request);

        var specification = new MemberSpecification(request);
        var members = (await _unitOfWork.MemberRepository.ListBySpecAsync(specification, cancellationToken)).ToList();
        var totalCount = await _unitOfWork.MemberRepository.CountBySpecAsync(new MemberSpecification(request, true), cancellationToken);

        _logger.LogInformation("Fetched {Count} members (Page {PageNumber}, PageSize {PageSize})",
            members.Count, request.PageNumber, request.PageSize);

        return Result<PaginationResult<MemberDto>>.Ok(new PaginationResult<MemberDto>(
            members.Select(_mapper.Map<MemberDto>).ToArray(),
            totalCount,
            request.PageNumber,
            Math.Ceiling((decimal)totalCount / request.PageSize),
            request.PageSize));
    }

    public async Task<Result<MemberDto?>> CreateAsync(CreateMemberRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new member: {@Request}", request);

        if (await _unitOfWork.MemberRepository.GetByEmailAsync(request.Email, cancellationToken) != null)
        {
            return Result<MemberDto?>.BadRequest("User with given email already exists");
        }

        var member = _mapper.Map<Member>(request);
        member.MemberId = Guid.CreateVersion7();

        await _unitOfWork.MemberRepository.AddAsync(member, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Member created successfully with ID: {MemberId}", member.MemberId);

        return Result<MemberDto?>.Created($"/api/members/{member.MemberId}", _mapper.Map<MemberDto>(member));
    }

    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to delete member with ID: {MemberId}", id);

        var member = await _unitOfWork.MemberRepository.GetByIdAsync(id, cancellationToken);
        if (member is null)
        {
            _logger.LogWarning("Cannot delete member. ID {MemberId} not found", id);
            return Result<bool>.NotFound(key: id, entityName: nameof(Member));
        }

        await _unitOfWork.MemberRepository.RemoveAsync(member);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Member with ID {MemberId} deleted successfully", id);

        return Result<bool>.NoContent();
    }

    public async Task<Result<MemberDto?>> UpdateAsync(Guid id, UpdateMemberRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating member with ID: {MemberId} using {@Request}", id, request);

        var member = await _unitOfWork.MemberRepository.GetByIdAsync(id, cancellationToken);
        if (member is null)
        {
            _logger.LogWarning("Cannot update member. ID {MemberId} not found", id);
            return Result<MemberDto?>.NotFound(key: id, entityName: nameof(Member));
        }

        member.FirstName = request.FirstName;
        member.LastName = request.LastName;
        member.Email = request.Email;
        member.PhoneNumber = request.PhoneNumber;

        await _unitOfWork.MemberRepository.UpdateAsync(member);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Member with ID {MemberId} updated successfully", id);

        return Result<MemberDto?>.Ok(_mapper.Map<MemberDto>(member));
    }
}
