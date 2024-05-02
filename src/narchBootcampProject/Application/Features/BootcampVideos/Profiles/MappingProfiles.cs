using Application.Features.BootcampImages.Commands.Create;
using Application.Features.BootcampImages.Commands.Delete;
using Application.Features.BootcampImages.Commands.Update;
using Application.Features.BootcampImages.Queries.GetById;
using Application.Features.BootcampImages.Queries.GetList;
using Application.Features.BootcampVideos.Commands.Create;
using Application.Features.BootcampVideos.Commands.Delete;
using Application.Features.BootcampVideos.Commands.Update;
using Application.Features.BootcampVideos.Queries.GetById;
using Application.Features.BootcampVideos.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BootcampVideos.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<BootcampVideo, CreateBootcampVideoCommand>().ReverseMap();
        CreateMap<BootcampVideo, CreatedBootcampVideoResponse>().ReverseMap();
        CreateMap<BootcampVideo, UpdateBootcampVideoCommand>().ReverseMap();
        CreateMap<BootcampVideo, UpdatedBootcampVideoResponse>().ReverseMap();
        CreateMap<BootcampVideo, DeleteBootcampVideoCommand>().ReverseMap();
        CreateMap<BootcampVideo, DeletedBootcampVideoResponse>().ReverseMap();
        CreateMap<BootcampVideo, GetByIdBootcampVideoResponse>().ReverseMap();
        CreateMap<BootcampVideo, GetListBootcampVideoListItemDto>().ReverseMap();
        CreateMap<IPaginate<BootcampVideo>, GetListResponse<GetListBootcampVideoListItemDto>>().ReverseMap();
    }
}
