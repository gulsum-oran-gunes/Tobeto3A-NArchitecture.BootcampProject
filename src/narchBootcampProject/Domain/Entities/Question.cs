using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Question : Entity<int>
{
    public int BootcampId { get; set; }
    public string Text { get; set; }
    public string AnswerA { get; set; }
    public string AnswerB { get; set; }
    public string AnswerC { get; set; }
    public string AnswerD { get; set; }
    public string CorrectAnswer { get; set; }
    [JsonIgnore]
    public virtual Bootcamp? Bootcamp { get; set; }

    [JsonIgnore]
    public ICollection<QuizQuestion> QuizQuestions { get; set; }

    public Question()
    {
        QuizQuestions = new HashSet<QuizQuestion>();
    }

    public Question(
        int bootcampId,
        string text,
        string answerA,
        string answerB,
        string answerC,
        string answerD,
        string correctAnswer
    )
    {
        BootcampId = bootcampId;
        Text = text;
        AnswerA = answerA;
        AnswerB = answerB;
        AnswerC = answerC;
        AnswerD = answerD;
        CorrectAnswer = correctAnswer;
    }
}
