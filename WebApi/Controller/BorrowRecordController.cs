using Domain.Dtos.BorrowRecordDto;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
public class BorrowRecordController(IBorrowRecordService service)
{
    [HttpPost]
    public async Task<Response<GetBorrowRecordDto>> AddBorrowRecord(CreateBorrowRecordDto recordDto){
        return await service.AddBorrowRecord(recordDto);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<GetBorrowRecordDto>> UpdateBorrowRecord(int id, UpdateBorrowRecordDto recordDto){
        return await service.UpdateBorrowRecord(id, recordDto);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteBorrowRecord(int id){
        return await service.DeleteBorrowRecord(id);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetBorrowRecordDto>> GetBorrowRecord(int id){
        return await service.GetBorrowRecord(id);
    }
    [HttpGet("overdue-records")]
    public async Task<Response<List<GetBorrowRecordDto>>> GetOverdueBorrowRecord(){
        return await service.GetOverdueBorrowRecord();
    }
    [HttpGet("memberId/{memberId:int}")]
    public async Task<Response<List<GetBorrowRecordDto>>> GetBorrowHistoryByMember(int memberId){
        return await service.GetBorrowHistoryByBook(memberId);
    }
    [HttpGet("{bookId}")]
    public async Task<Response<List<GetBorrowRecordDto>>> GetBorrowHistoryByBook(int bookId){
        return await service.GetBorrowHistoryByBook(bookId);
    }
}
