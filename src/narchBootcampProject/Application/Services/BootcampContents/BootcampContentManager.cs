using Application.Features.BootcampContents.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.BootcampContents;

public class BootcampContentManager : IBootcampContentService
{
    private readonly IBootcampContentRepository _bootcampContentRepository;
    private readonly BootcampContentBusinessRules _bootcampContentBusinessRules;

    public BootcampContentManager(IBootcampContentRepository bootcampContentRepository, BootcampContentBusinessRules bootcampContentBusinessRules)
    {
        _bootcampContentRepository = bootcampContentRepository;
        _bootcampContentBusinessRules = bootcampContentBusinessRules;
    }

    public async Task<BootcampContent?> GetAsync(
        Expression<Func<BootcampContent, bool>> predicate,
        Func<IQueryable<BootcampContent>, IIncludableQueryable<BootcampContent, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        BootcampContent? bootcampContent = await _bootcampContentRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return bootcampContent;
    }

    public async Task<IPaginate<BootcampContent>?> GetListAsync(
        Expression<Func<BootcampContent, bool>>? predicate = null,
        Func<IQueryable<BootcampContent>, IOrderedQueryable<BootcampContent>>? orderBy = null,
        Func<IQueryable<BootcampContent>, IIncludableQueryable<BootcampContent, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<BootcampContent> bootcampContentList = await _bootcampContentRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return bootcampContentList;
    }

    public async Task<BootcampContent> AddAsync(BootcampContent bootcampContent)
    {
        BootcampContent addedBootcampContent = await _bootcampContentRepository.AddAsync(bootcampContent);

        return addedBootcampContent;
    }

    public async Task<BootcampContent> UpdateAsync(BootcampContent bootcampContent)
    {
        BootcampContent updatedBootcampContent = await _bootcampContentRepository.UpdateAsync(bootcampContent);

        return updatedBootcampContent;
    }

    public async Task<BootcampContent> DeleteAsync(BootcampContent bootcampContent, bool permanent = false)
    {
        BootcampContent deletedBootcampContent = await _bootcampContentRepository.DeleteAsync(bootcampContent);

        return deletedBootcampContent;
    }
}
