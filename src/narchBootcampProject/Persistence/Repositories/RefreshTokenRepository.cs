using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;
using Persistence.Migrations;

namespace Persistence.Repositories;

public class RefreshTokenRepository : EfRepositoryBase<RefreshToken, Guid, BaseDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(BaseDbContext context)
        : base(context) { }

    public async Task<List<RefreshToken>> GetOldRefreshTokensAsync(Guid userId, int refreshTokenTtl)
    {
        List<RefreshToken> tokens = await Query()
            .AsNoTracking()
            .Where(r =>
                r.UserId == userId
                && r.RevokedDate == null
                && r.ExpiresDate >= DateTime.UtcNow
                && r.CreatedDate.AddDays(refreshTokenTtl) <= DateTime.UtcNow
            )
            .ToListAsync();

        return tokens;
    }

    public async Task<List<RefreshToken>> GetRefreshTokenByUserIdAsync(Guid userId)
    {
        List<RefreshToken> tokens = await Query()
            .AsNoTracking()
            .Where(r =>
                r.UserId == userId)
            .ToListAsync();

        return tokens;
    }

    //var quizQuestions = await _quizQuestionRepository.GetAllAsync(include: x =>
    //       x /*.Include(x => x.Quiz).*/
    //       .Include(x => x.Question)
    //   );
    //var finalQuestions = quizQuestions.Where(q => q.QuizId == quizId);

    //    return finalQuestions.ToList();
}
