using Application.Features.ApplicantBootcampContents.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ApplicantBootcampContents;

public class ApplicantBootcampContentManager : IApplicantBootcampContentService
{
    private readonly IApplicantBootcampContentRepository _applicantBootcampContentRepository;
    private readonly ApplicantBootcampContentBusinessRules _applicantBootcampContentBusinessRules;

    public ApplicantBootcampContentManager(IApplicantBootcampContentRepository applicantBootcampContentRepository, ApplicantBootcampContentBusinessRules applicantBootcampContentBusinessRules)
    {
        _applicantBootcampContentRepository = applicantBootcampContentRepository;
        _applicantBootcampContentBusinessRules = applicantBootcampContentBusinessRules;
    }

    public async Task<ApplicantBootcampContent?> GetAsync(
        Expression<Func<ApplicantBootcampContent, bool>> predicate,
        Func<IQueryable<ApplicantBootcampContent>, IIncludableQueryable<ApplicantBootcampContent, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ApplicantBootcampContent? applicantBootcampContent = await _applicantBootcampContentRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return applicantBootcampContent;
    }

    public async Task<IPaginate<ApplicantBootcampContent>?> GetListAsync(
        Expression<Func<ApplicantBootcampContent, bool>>? predicate = null,
        Func<IQueryable<ApplicantBootcampContent>, IOrderedQueryable<ApplicantBootcampContent>>? orderBy = null,
        Func<IQueryable<ApplicantBootcampContent>, IIncludableQueryable<ApplicantBootcampContent, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ApplicantBootcampContent> applicantBootcampContentList = await _applicantBootcampContentRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return applicantBootcampContentList;
    }

    public async Task<ApplicantBootcampContent> AddAsync(ApplicantBootcampContent applicantBootcampContent)
    {
        ApplicantBootcampContent addedApplicantBootcampContent = await _applicantBootcampContentRepository.AddAsync(applicantBootcampContent);

        return addedApplicantBootcampContent;
    }

    public async Task<ApplicantBootcampContent> UpdateAsync(ApplicantBootcampContent applicantBootcampContent)
    {
        ApplicantBootcampContent updatedApplicantBootcampContent = await _applicantBootcampContentRepository.UpdateAsync(applicantBootcampContent);

        return updatedApplicantBootcampContent;
    }

    public async Task<ApplicantBootcampContent> DeleteAsync(ApplicantBootcampContent applicantBootcampContent, bool permanent = false)
    {
        ApplicantBootcampContent deletedApplicantBootcampContent = await _applicantBootcampContentRepository.DeleteAsync(applicantBootcampContent);

        return deletedApplicantBootcampContent;
    }
}
