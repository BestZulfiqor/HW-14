using System.Net;
using Domain.Dtos.BorrowRecordDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BorrowRecordService(DataContext context) : IBorrowRecordService
{
    public async Task<Response<GetBorrowRecordDto>> AddBorrowRecord(CreateBorrowRecordDto recordDto)
    {
        var borrow = new BorrowRecord()
        {
            MemberId = recordDto.MemberId,
            BookId = recordDto.BookId,
            BorrowDate = recordDto.BorrowDate,
            ReturnDate = recordDto.ReturnDate
        };

        await context.BorrowRecords.AddAsync(borrow);
        var result = await context.SaveChangesAsync();
        var getBorrow = new GetBorrowRecordDto()
        {
            Id = borrow.Id,
            MemberId = recordDto.MemberId,
            BookId = recordDto.BookId,
            BorrowDate = recordDto.BorrowDate,
            ReturnDate = recordDto.ReturnDate
        };

        return result == 0
            ? new Response<GetBorrowRecordDto>(HttpStatusCode.BadRequest, "Borrow not added!")
            : new Response<GetBorrowRecordDto>(getBorrow);
    }

    public async Task<Response<string>> DeleteBorrowRecord(int id)
    {
        var exist = await context.BorrowRecords.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Borrow not found");
        }

        context.BorrowRecords.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Borrow not deleted!")
            : new Response<string>("Borrow deleted!");
    }

    public async Task<Response<List<GetBorrowRecordDto>>> GetBorrowHistoryByBook(int bookId)
    {
        var exist = await context.BorrowRecords.FirstOrDefaultAsync(n => n.BookId == bookId);
        if (exist == null)
        {
            return new Response<List<GetBorrowRecordDto>>(HttpStatusCode.NotFound, "Book not found");
        }
        var borrows = await context.BorrowRecords
            .Where(n => n.BookId == bookId)
            .Select(n => new GetBorrowRecordDto()
            {
                Id = n.Id,
                MemberId = n.MemberId,
                BookId = n.BookId,
                BorrowDate = n.BorrowDate,
                ReturnDate = n.ReturnDate
            })
            .ToListAsync();

        return new Response<List<GetBorrowRecordDto>>(borrows);
    }

    public async Task<Response<List<GetBorrowRecordDto>>> GetBorrowHistoryByMember(int memberId)
    {
        var exist = await context.BorrowRecords.FirstOrDefaultAsync(n => n.MemberId == memberId);
        if (exist == null)
        {
            return new Response<List<GetBorrowRecordDto>>(HttpStatusCode.NotFound, "Member not found");
        }
        var borrows = await context.BorrowRecords
            .Where(n => n.MemberId == memberId)
            .Select(n => new GetBorrowRecordDto()
            {
                Id = n.Id,
                MemberId = n.MemberId,
                BookId = n.BookId,
                BorrowDate = n.BorrowDate,
                ReturnDate = n.ReturnDate
            })
            .ToListAsync();

        return new Response<List<GetBorrowRecordDto>>(borrows);
    }

    public async Task<Response<GetBorrowRecordDto>> GetBorrowRecord(int id)
    {
        var exist = await context.BorrowRecords.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetBorrowRecordDto>(HttpStatusCode.NotFound, "Borrow not found!");
        }

        var borrow = new GetBorrowRecordDto()
        {
            Id = exist.Id,
            MemberId = exist.MemberId,
            BookId = exist.BookId,
            BorrowDate = exist.BorrowDate,
            ReturnDate = exist.ReturnDate
        };

        return new Response<GetBorrowRecordDto>(borrow);
    }

    public async Task<Response<List<GetBorrowRecordDto>>> GetOverdueBorrowRecord()
    {
        var borrow = await context.BorrowRecords
            .Where(n => DateTime.Now >= n.ReturnDate)
            .Select(n => new GetBorrowRecordDto{
                Id = n.Id,
                BookId = n.BookId,
                MemberId = n.MemberId,
                ReturnDate = n.ReturnDate,
                BorrowDate = n.BorrowDate
            })
            .ToListAsync();
        
        return new Response<List<GetBorrowRecordDto>>(borrow);
    }

    public async Task<Response<GetBorrowRecordDto>> UpdateBorrowRecord(int id, UpdateBorrowRecordDto recordDto)
    {
        var exist = await context.BorrowRecords.FindAsync(recordDto);
        if (exist == null)
        {
            return new Response<GetBorrowRecordDto>(HttpStatusCode.NotFound, "Borrow not found");
        }

        exist.BookId = recordDto.BookId;
        exist.MemberId = recordDto.MemberId;
        exist.BorrowDate = recordDto.BorrowDate;
        exist.ReturnDate = recordDto.ReturnDate;
        var result = await context.SaveChangesAsync();
        var borrow = new GetBorrowRecordDto()
        {
            Id = exist.Id,
            MemberId = exist.MemberId,
            BookId = exist.BookId,
            BorrowDate = exist.BorrowDate,
            ReturnDate = exist.ReturnDate
        };

        return result == 0
            ? new Response<GetBorrowRecordDto>(HttpStatusCode.BadRequest, "Borrow not updated")
            : new Response<GetBorrowRecordDto>(borrow);
    }

}
