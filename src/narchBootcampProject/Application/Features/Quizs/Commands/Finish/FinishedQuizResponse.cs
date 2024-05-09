using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Quizs.Commands.Finish;

public class FinishedQuizResponse : IResponse
{
    public Result Result { get; set; }
    public List<QuestionResult> QuestionResults { get; set; }
    public int Score => Result.CorrectAnswers * 20;
}
