using Application.Features.Instructors.Commands.Create;
using Application.Features.Instructors.Commands.Delete;
using Application.Features.Instructors.Commands.Update;
using Application.Features.Instructors.Queries.GetById;
using Application.Features.Instructors.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Instructors.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Instructor, CreateInstructorCommand>().ReverseMap();
        CreateMap<Instructor, CreatedInstructorResponse>().ReverseMap();
        CreateMap<Instructor, UpdateInstructorCommand>().ReverseMap();
        CreateMap<Instructor, UpdatedInstructorResponse>().ReverseMap();
        CreateMap<Instructor, DeleteInstructorCommand>().ReverseMap();
        CreateMap<Instructor, DeletedInstructorResponse>().ReverseMap();
        CreateMap<Instructor, GetByIdInstructorResponse>()
        .ForMember(destinationMember: x => x.InstructorImagePath, memberOptions: opt => opt.MapFrom(x => x.InstructorImages.FirstOrDefault().ImagePath))
        .ForMember(destinationMember: x => x.InstructorImageId, memberOptions: opt => opt.MapFrom(x => x.InstructorImages.FirstOrDefault().Id));
       
        CreateMap<Instructor, GetListInstructorListItemDto>()
        .ForMember(destinationMember: x => x.InstructorImagePath, memberOptions: opt => opt.MapFrom(x => x.InstructorImages.FirstOrDefault().ImagePath))
        .ForMember(destinationMember: x => x.InstructorImageId, memberOptions: opt => opt.MapFrom(x => x.InstructorImages.FirstOrDefault().Id));
       
        CreateMap<IPaginate<Instructor>, GetListResponse<GetListInstructorListItemDto>>().ReverseMap();
    
    }
}
