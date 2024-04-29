using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class QuizQuestion : Entity<int>
{
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public virtual Quiz Quiz { get; set; }
    public virtual Question Question { get; set; }

    public QuizQuestion(int quizId, int questionId)
    {
        QuizId = quizId;
        QuestionId = questionId;
    }
}
