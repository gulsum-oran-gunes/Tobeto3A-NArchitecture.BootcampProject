using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ApplicantBootcampContents;

public interface IApplicantBootcampContentService
{
    Task<ApplicantBootcampContent?> GetAsync(
        Expression<Func<ApplicantBootcampContent, bool>> predicate,
        Func<IQueryable<ApplicantBootcampContent>, IIncludableQueryable<ApplicantBootcampContent, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ApplicantBootcampContent>?> GetListAsync(
        Expression<Func<ApplicantBootcampContent, bool>>? predicate = null,
        Func<IQueryable<ApplicantBootcampContent>, IOrderedQueryable<ApplicantBootcampContent>>? orderBy = null,
        Func<IQueryable<ApplicantBootcampContent>, IIncludableQueryable<ApplicantBootcampContent, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ApplicantBootcampContent> AddAsync(ApplicantBootcampContent applicantBootcampContent);
    Task<ApplicantBootcampContent> UpdateAsync(ApplicantBootcampContent applicantBootcampContent);
    Task<ApplicantBootcampContent> DeleteAsync(ApplicantBootcampContent applicantBootcampContent, bool permanent = false);
}
