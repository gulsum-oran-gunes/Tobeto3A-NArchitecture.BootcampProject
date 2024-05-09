using Application.Features.BootcampContents.Commands.Create;
using Application.Features.BootcampContents.Commands.Delete;
using Application.Features.BootcampContents.Commands.Update;
using Application.Features.BootcampContents.Queries.GetById;
using Application.Features.BootcampContents.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.BootcampContents.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<BootcampContent, CreateBootcampContentCommand>().ReverseMap();
        CreateMap<BootcampContent, CreatedBootcampContentResponse>().ReverseMap();
        CreateMap<BootcampContent, UpdateBootcampContentCommand>().ReverseMap();
        CreateMap<BootcampContent, UpdatedBootcampContentResponse>().ReverseMap();
        CreateMap<BootcampContent, DeleteBootcampContentCommand>().ReverseMap();
        CreateMap<BootcampContent, DeletedBootcampContentResponse>().ReverseMap();
        CreateMap<BootcampContent, GetByIdBootcampContentResponse>().ReverseMap();
        CreateMap<BootcampContent, GetListBootcampContentListItemDto>().ReverseMap();
        CreateMap<IPaginate<BootcampContent>, GetListResponse<GetListBootcampContentListItemDto>>().ReverseMap();
    }
}