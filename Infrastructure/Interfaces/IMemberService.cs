using Domain.Dtos.MemberDto;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IMemberService
{
    Task<Response<GetMemberDto>> AddMember(CreateMemberDto memberDto);
    Task<Response<GetMemberDto>> UpdateMember(int id, UpdateMemberDto memberDto);
    Task<Response<string>> DeleteMember(int id);
    Task<Response<GetMemberDto>> GetMember(int id); 
    Task<Response<List<GetMemberDto>>> GetMemberWithRecentBorrows(int days);
    Task<Response<List<GetMemberDto>>> GetTopNMemberByBorrows(int n);
}
