using Application.Features.ApplicantBootcampContents.Commands.Create;
using Application.Features.ApplicantBootcampContents.Commands.Delete;
using Application.Features.ApplicantBootcampContents.Commands.Update;
using Application.Features.ApplicantBootcampContents.Queries.GetById;
using Application.Features.ApplicantBootcampContents.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.ApplicantBootcampContents.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ApplicantBootcampContent, CreateApplicantBootcampContentCommand>().ReverseMap();
        CreateMap<ApplicantBootcampContent, CreatedApplicantBootcampContentResponse>().ReverseMap();
        CreateMap<ApplicantBootcampContent, UpdateApplicantBootcampContentCommand>().ReverseMap();
        CreateMap<ApplicantBootcampContent, UpdatedApplicantBootcampContentResponse>().ReverseMap();
        CreateMap<ApplicantBootcampContent, DeleteApplicantBootcampContentCommand>().ReverseMap();
        CreateMap<ApplicantBootcampContent, DeletedApplicantBootcampContentResponse>().ReverseMap();
        CreateMap<ApplicantBootcampContent, GetByIdApplicantBootcampContentResponse>().ReverseMap();
        CreateMap<ApplicantBootcampContent, GetListApplicantBootcampContentListItemDto>().ReverseMap();
        CreateMap<IPaginate<ApplicantBootcampContent>, GetListResponse<GetListApplicantBootcampContentListItemDto>>().ReverseMap();
    }
}