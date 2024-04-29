using System.Linq.Expressions;
using Application.Features.Results.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.Results;

public class ResultManager : IResultService
{
    private readonly IResultRepository _resultRepository;
    private readonly ResultBusinessRules _resultBusinessRules;

    public ResultManager(IResultRepository resultRepository, ResultBusinessRules resultBusinessRules)
    {
        _resultRepository = resultRepository;
        _resultBusinessRules = resultBusinessRules;
    }

    public async Task<Result?> GetAsync(
        Expression<Func<Result, bool>> predicate,
        Func<IQueryable<Result>, IIncludableQueryable<Result, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Result? result = await _resultRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return result;
    }

    public async Task<IPaginate<Result>?> GetListAsync(
        Expression<Func<Result, bool>>? predicate = null,
        Func<IQueryable<Result>, IOrderedQueryable<Result>>? orderBy = null,
        Func<IQueryable<Result>, IIncludableQueryable<Result, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Result> resultList = await _resultRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return resultList;
    }

    public async Task<Result> AddAsync(Result result)
    {
        Result addedResult = await _resultRepository.AddAsync(result);

        return addedResult;
    }

    public async Task<Result> UpdateAsync(Result result)
    {
        Result updatedResult = await _resultRepository.UpdateAsync(result);

        return updatedResult;
    }

    public async Task<Result> DeleteAsync(Result result, bool permanent = false)
    {
        Result deletedResult = await _resultRepository.DeleteAsync(result);

        return deletedResult;
    }
}
