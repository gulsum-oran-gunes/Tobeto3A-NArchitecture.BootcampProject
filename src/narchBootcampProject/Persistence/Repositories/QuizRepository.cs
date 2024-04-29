using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class QuizRepository : EfRepositoryBase<Quiz, int, BaseDbContext>, IQuizRepository
{
    public QuizRepository(BaseDbContext context)
        : base(context) { }
}
