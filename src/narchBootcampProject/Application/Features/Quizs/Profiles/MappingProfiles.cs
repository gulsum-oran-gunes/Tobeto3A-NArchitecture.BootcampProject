using Application.Features.Quizs.Commands.Create;
using Application.Features.Quizs.Commands.Delete;
using Application.Features.Quizs.Commands.Finish;
using Application.Features.Quizs.Commands.Update;
using Application.Features.Quizs.Queries.GetById;
using Application.Features.Quizs.Queries.GetList;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Quizs.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Quiz, CreateQuizCommand>().ReverseMap();
        CreateMap<Quiz, CreatedQuizResponse>().ForMember(q => q.BootcampName, opt => opt.MapFrom(q => q.Bootcamp.Name));
        CreateMap<Quiz, UpdateQuizCommand>().ReverseMap();
        CreateMap<Quiz, UpdatedQuizResponse>().ReverseMap();
        CreateMap<Quiz, DeleteQuizCommand>().ReverseMap();
        CreateMap<Quiz, DeletedQuizResponse>().ReverseMap();
        CreateMap<Quiz, GetByIdQuizResponse>().ForMember(q => q.Questions, opt => opt.MapFrom(q => q.QuizQuestions.Select(x => x.Question)));
        CreateMap<Quiz, GetListQuizListItemDto>().ForMember(q => q.Questions, opt => opt.MapFrom(q => q.QuizQuestions.Select(x => x.Question)));
        CreateMap<IPaginate<Quiz>, GetListResponse<GetListQuizListItemDto>>().ReverseMap();
        CreateMap<Quiz, FinishQuizCommand>().ReverseMap();
        CreateMap<Result, FinishedQuizResponse>().ReverseMap();
    }
}
