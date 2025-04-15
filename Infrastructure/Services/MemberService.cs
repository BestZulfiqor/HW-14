using System.Net;
using Domain.Dtos;
using Domain.Dtos.MemberDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        var exists = await context.Members.FindAsync(id);
        if (exists == null)
        {
            return new Response<string>("Not found member");
        }
        context.Members.Remove(exists);

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Member not delete")
            : new Response<string>(HttpStatusCode.OK, "Member deleted succesfully");
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

    public async Task<Response<List<GetMemberBorrowDate>>> GetMemberWithRecentBorrows(int days)
    {
        var members = await (
            from member in context.Members
            join borrow in context.BorrowRecords on member.Id equals borrow.MemberId
            where borrow.BorrowDate.Day >= DateTime.Now.AddDays(-days).Day
            select new GetMemberBorrowDate
            {
                Name = member.Name,
                Email = member.Email,
                BorrowDate = borrow.BorrowDate
            }
        ).ToListAsync();

        return new Response<List<GetMemberBorrowDate>>(members);
    }

    public async Task<Response<List<GetMemberCountBook>>> GetTopNMemberByBorrows(int n)
    {
        var members = await (
            from member in context.Members
            join borrow in context.BorrowRecords on member.Id equals borrow.MemberId
            orderby member.BorrowRecords.Count descending
            select new GetMemberCountBook
            {
                Name = member.Name,
                Email = member.Email,
                Count = member.BorrowRecords.Count
            }
        )
        .Take(n)
        .ToListAsync();

        return new Response<List<GetMemberCountBook>>(members);
    }
}
