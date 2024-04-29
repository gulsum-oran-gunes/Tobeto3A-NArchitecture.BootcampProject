using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.Results;

public interface IResultService
{
    Task<Result?> GetAsync(
        Expression<Func<Result, bool>> predicate,
        Func<IQueryable<Result>, IIncludableQueryable<Result, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Result>?> GetListAsync(
        Expression<Func<Result, bool>>? predicate = null,
        Func<IQueryable<Result>, IOrderedQueryable<Result>>? orderBy = null,
        Func<IQueryable<Result>, IIncludableQueryable<Result, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Result> AddAsync(Result result);
    Task<Result> UpdateAsync(Result result);
    Task<Result> DeleteAsync(Result result, bool permanent = false);
}
