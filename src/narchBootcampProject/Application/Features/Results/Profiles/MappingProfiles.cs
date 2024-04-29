using Application.Features.Results.Commands.Create;
using Application.Features.Results.Commands.Delete;
using Application.Features.Results.Commands.Update;
using Application.Features.Results.Queries.GetById;
using Application.Features.Results.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Results.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Result, CreateResultCommand>().ReverseMap();
        CreateMap<Result, CreatedResultResponse>().ReverseMap();
        CreateMap<Result, UpdateResultCommand>().ReverseMap();
        CreateMap<Result, UpdatedResultResponse>().ReverseMap();
        CreateMap<Result, DeleteResultCommand>().ReverseMap();
        CreateMap<Result, DeletedResultResponse>().ReverseMap();
        CreateMap<Result, GetByIdResultResponse>().ReverseMap();
        CreateMap<Result, GetListResultListItemDto>().ReverseMap();
        CreateMap<IPaginate<Result>, GetListResponse<GetListResultListItemDto>>().ReverseMap();
    }
}
