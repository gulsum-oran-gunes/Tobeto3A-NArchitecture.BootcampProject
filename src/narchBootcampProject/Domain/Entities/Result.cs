using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Result : Entity<int>
{
    public int QuizId { get; set; }
    public int WrongAnswers { get; set; }
    public int CorrectAnswers { get; set; }
    public bool? IsPassed { get; set; }
    public virtual Quiz? Quiz { get; set; }

    public Result() { }

    public Result(int quizId, int wrongAnswers, int correctAnswers, bool? ısPassed)
    {
        QuizId = quizId;
        WrongAnswers = wrongAnswers;
        CorrectAnswers = correctAnswers;
        IsPassed = ısPassed;
    }
}
