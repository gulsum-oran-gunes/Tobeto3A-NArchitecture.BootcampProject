using Application.Features.InstructorImages.Commands.Create;
using Application.Features.InstructorImages.Commands.Delete;
using Application.Features.InstructorImages.Commands.Update;
using Application.Features.InstructorImages.Queries.GetById;
using Application.Features.InstructorImages.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Services.BootcampImages;
using Application.Services.InstructorImages;

namespace Application.Features.InstructorImages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<InstructorImage, CreateInstructorImageCommand>().ReverseMap();
        CreateMap<InstructorImage, CreatedInstructorImageResponse>().ReverseMap();
        CreateMap<InstructorImage, UpdateInstructorImageCommand>().ReverseMap();
        CreateMap<InstructorImage, UpdatedInstructorImageResponse>().ReverseMap();
        CreateMap<InstructorImage, DeleteInstructorImageCommand>().ReverseMap();
        CreateMap<InstructorImage, DeletedInstructorImageResponse>().ReverseMap();
        CreateMap<InstructorImage, GetByIdInstructorImageResponse>().ReverseMap();
        CreateMap<InstructorImage, GetListInstructorImageListItemDto>().ReverseMap();
        CreateMap<IPaginate<InstructorImage>, GetListResponse<GetListInstructorImageListItemDto>>().ReverseMap();
        CreateMap<UpdateInstructorImageRequest, InstructorImage>().ReverseMap();
    }
}