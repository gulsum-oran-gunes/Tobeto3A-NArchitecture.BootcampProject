using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.BootcampContents;

public interface IBootcampContentService
{
    Task<BootcampContent?> GetAsync(
        Expression<Func<BootcampContent, bool>> predicate,
        Func<IQueryable<BootcampContent>, IIncludableQueryable<BootcampContent, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<BootcampContent>?> GetListAsync(
        Expression<Func<BootcampContent, bool>>? predicate = null,
        Func<IQueryable<BootcampContent>, IOrderedQueryable<BootcampContent>>? orderBy = null,
        Func<IQueryable<BootcampContent>, IIncludableQueryable<BootcampContent, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<BootcampContent> AddAsync(BootcampContent bootcampContent);
    Task<BootcampContent> UpdateAsync(BootcampContent bootcampContent);
    Task<BootcampContent> DeleteAsync(BootcampContent bootcampContent, bool permanent = false);
}
