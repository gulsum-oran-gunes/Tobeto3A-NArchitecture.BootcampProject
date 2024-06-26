using Application.Features.Bootcamps.Commands.Create;
using Application.Features.Bootcamps.Commands.Delete;
using Application.Features.Bootcamps.Commands.Update;
using Application.Features.Bootcamps.Queries.GetById;
using Application.Features.Bootcamps.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Bootcamps.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Bootcamp, CreateBootcampCommand>().ReverseMap();
        CreateMap<Bootcamp, CreatedBootcampResponse>().ReverseMap();
        CreateMap<Bootcamp, UpdateBootcampCommand>().ReverseMap();
        CreateMap<Bootcamp, UpdatedBootcampResponse>().ReverseMap();
        CreateMap<Bootcamp, DeleteBootcampCommand>().ReverseMap();
        CreateMap<Bootcamp, DeletedBootcampResponse>().ReverseMap();
        CreateMap<Bootcamp, GetByIdBootcampResponse>()
        .ForMember(destinationMember: x => x.BootcampImagePath, memberOptions: opt => opt.MapFrom(x => x.BootcampImages.FirstOrDefault().ImagePath))
        .ForMember(destinationMember: x => x.BootcampImageId, memberOptions: opt => opt.MapFrom(x => x.BootcampImages.FirstOrDefault().Id))
        .ForMember(destinationMember: x => x.InstructorImageId, memberOptions: opt => opt.MapFrom(x => x.Instructor.InstructorImages.FirstOrDefault().Id))
        .ForMember(destinationMember: x => x.InstructorImagePath, memberOptions: opt => opt.MapFrom(x => x.Instructor.InstructorImages.FirstOrDefault().ImagePath));
        
        CreateMap<Bootcamp, GetListBootcampListItemDto>()
       .ForMember(destinationMember: x => x.BootcampImagePath, memberOptions: opt => opt.MapFrom(x => x.BootcampImages.FirstOrDefault().ImagePath))
       .ForMember(destinationMember: x => x.BootcampImageId, memberOptions: opt => opt.MapFrom(x => x.BootcampImages.FirstOrDefault().Id));

        CreateMap<IPaginate<Bootcamp>, GetListResponse<GetListBootcampListItemDto>>().ReverseMap();
    }
}
