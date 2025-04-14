using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
public class BorrowRecordController(IBorrowRecordService service)
{
}
