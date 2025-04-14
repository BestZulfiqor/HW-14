using System.Net;
using Domain.Dtos.MemberDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class MemberService(DataContext context) : IMemberService
{
    public async Task<Response<GetMemberDto>> AddMember(CreateMemberDto memberDto)
    {
        var memberdto = new Member()
        {
            Name = memberDto.Name,
            Email = memberDto.Email,
        };

        await context.Members.AddAsync(memberdto);
        var res = await context.SaveChangesAsync();

        var get = new GetMemberDto()
        {
            Email = memberDto.Email,
            Name = memberDto.Name,
        };

        return res == 0
            ? new Response<GetMemberDto>(HttpStatusCode.BadRequest, "Not added")
            : new Response<GetMemberDto>(get);

    }

    public async Task<Response<GetMemberDto>> UpdateMember(int id, UpdateMemberDto memberDto)
    {
        var member = await context.Members.FindAsync(id);

        if (member == null)
        {
            return new Response<GetMemberDto>(HttpStatusCode.NotFound, "Member not found");
        }

        member.Email = memberDto.Email;
        member.Name = memberDto.Name;

        var res = await context.SaveChangesAsync();

        var result = new GetMemberDto()
        {
            Email = memberDto.Email,
            Name = memberDto.Name,
        };

        return new Response<GetMemberDto>(result);

    }

    public async Task<Response<string>> DeleteMember(int id)
    {
        var exists = context.Members.Find(id);

        context.Members.Remove(exists);

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Did not delete")
            : new Response<string>(HttpStatusCode.OK, "Member deleted");
    }

    public async Task<Response<GetMemberDto>> GetMember(int id)
    {
        var member = await context.Members.FindAsync(id);

        if (member == null)
        {
            return new Response<GetMemberDto>(HttpStatusCode.NotFound, "Member not found");
        }

        var res = new GetMemberDto()
        {
            Email = member.Email,
            Name = member.Name,
        };

        return new Response<GetMemberDto>(res);
    }

    public Task<Response<List<GetMemberDto>>> GetMemberWithRecentBorrows(int days)
    {
        throw new Exception();
    }

    public Task<Response<List<GetMemberDto>>> GetTopNMemberByBorrows(int n)
    {
        throw new Exception();
    }
}
