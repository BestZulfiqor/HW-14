using Domain.Dtos;
using Domain.Dtos.MemberDto;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
public class MemberController(IMemberService service)
{
    [HttpPost]
    public async Task<Response<GetMemberDto>> AddMember(CreateMemberDto memberDto)
    {
        return await service.AddMember(memberDto);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<GetMemberDto>> UpdateMember(int id, UpdateMemberDto memberDto)
    {
        return await service.UpdateMember(id, memberDto);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteMember(int id)
    {
        return await service.DeleteMember(id);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetMemberDto>> GetMember(int id){
        return await service.GetMember(id);
    }
    [HttpGet("days/{days:int}")]
    public async Task<Response<List<GetMemberBorrowDate>>> GetMemberWithRecentBorrows(int days)
    {
        return await service.GetMemberWithRecentBorrows(days);
    }
    [HttpGet("top{n:int}")]
    public async Task<Response<List<GetMemberCountBook>>> GetTopNMemberByBorrows(int n)
    {
        return await service.GetTopNMemberByBorrows(n);
    }

}
