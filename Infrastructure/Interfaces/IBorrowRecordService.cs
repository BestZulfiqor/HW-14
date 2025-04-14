using Domain.Dtos.BorrowRecordDto;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IBorrowRecordService
{
    Task<Response<GetBorrowRecordDto>> AddBorrowRecord(CreateBorrowRecordDto recordDto);
    Task<Response<GetBorrowRecordDto>> UpdateBorrowRecord(int id, UpdateBorrowRecordDto recordDto);
    Task<Response<string>> DeleteBorrowRecord(int id);
    Task<Response<GetBorrowRecordDto>> GetBorrowRecord(int id);
    Task<Response<List<GetBorrowRecordDto>>> GetOverdueBorrowRecord();
    Task<Response<List<GetBorrowRecordDto>>> GetBorrowHistoryByMember(int memberId);
    Task<Response<List<GetBorrowRecordDto>>> GetBorrowHistoryByBook(int bookId);
}
