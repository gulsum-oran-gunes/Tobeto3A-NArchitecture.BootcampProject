using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class QuestionRepository : EfRepositoryBase<Question, int, BaseDbContext>, IQuestionRepository
{
    private readonly BaseDbContext _context;

    public QuestionRepository(BaseDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<List<Question>> GetAllAsync()
    {
        return await _context.Questions.ToListAsync();
    }
}
