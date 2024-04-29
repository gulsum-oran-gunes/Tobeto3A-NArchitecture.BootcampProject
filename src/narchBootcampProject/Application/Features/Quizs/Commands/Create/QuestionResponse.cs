using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Quizs.Commands.Create;

public class QuestionResponse : IResponse
{
    public int Id { get; set; }
    public int BootcampId { get; set; }
    public string Text { get; set; }
    public string AnswerA { get; set; }
    public string AnswerB { get; set; }
    public string AnswerC { get; set; }
    public string AnswerD { get; set; }
}
