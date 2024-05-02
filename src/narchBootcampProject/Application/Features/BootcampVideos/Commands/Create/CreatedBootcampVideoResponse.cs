using Application.Features.BootcampImages.Commands.Create;
using Application.Features.BootcampImages.Constants;
using Application.Features.BootcampImages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.Application.Responses;


namespace Application.Features.BootcampVideos.Commands.Create;
public class CreatedBootcampVideoResponse : IResponse
{
    public int Id { get; set; }
    public int BootcampId { get; set; }
    public string ThumbnailUrl { get; set; }
}