using NArchitecture.Core.Security.Attributes;

namespace Application.Features.Quizs.Constants;

[OperationClaimConstants]
public static class QuizsOperationClaims
{
    private const string _section = "Quizs";

    public const string Admin = $"{_section}.Admin";

    public const string Read = $"{_section}.Read";
    public const string Write = $"{_section}.Write";

    public const string Create = $"{_section}.Create";
    public const string Update = $"{_section}.Update";
    public const string Delete = $"{_section}.Delete";
    public const string Finish = $"{_section}.Finish";
    public const string Student = "Student";

}
