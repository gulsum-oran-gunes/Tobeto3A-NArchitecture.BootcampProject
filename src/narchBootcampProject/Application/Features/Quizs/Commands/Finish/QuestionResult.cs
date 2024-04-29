using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Quizs.Commands.Finish;

public class QuestionResult : IResponse
{
    public int Id { get; set; }
    public string CorrectAnswer { get; set; }
    public string StudentAnswer { get; set; }
}
