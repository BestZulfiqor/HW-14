using AutoMapper;
using Domain.Dtos;
using Domain.Dtos.AuthorDto;
using Domain.Dtos.BookDto;
using Domain.Dtos.BorrowRecordDto;
using Domain.Dtos.MemberDto;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Author, GetAuthorDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src=>src.Name/* + " " + src.LastName*/));
        
        CreateMap<Author, GetAuthorDto>();
        CreateMap<CreateAuthorDto, Author>();
        CreateMap<Author, GetAuthorsWithMostBooksDto>();
        CreateMap<Member, GetMemberBorrowDate>();
        CreateMap<Member, GetMemberCountBook>();
        CreateMap<Member, GetMemberDto>();
        CreateMap<GetMemberDto, Member>();
        CreateMap<Book, GetBookDto>();
        CreateMap<GetBookDto, Book>();
        CreateMap<BorrowRecord, GetBorrowRecordDto>();
        CreateMap<GetBorrowRecordDto, BorrowRecord>();
    }
}
