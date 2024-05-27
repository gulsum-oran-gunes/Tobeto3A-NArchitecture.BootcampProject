using Application.Features.Auth.Commands.VerifyEmail;
using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Features.Auth.Commands.VerifyEmail;
public class VerifyEmailCommand : IRequest
{
    public Guid UserId { get; set; }
    public string ActivationCode { get; set; }

    public VerifyEmailCommand()
    {
        ActivationCode = string.Empty;
    }

    public VerifyEmailCommand(Guid userId, string activationCode)
    {
        UserId = userId;
        ActivationCode = activationCode;
    }

    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand>
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly AuthBusinessRules _authBusinessRules;

        public VerifyEmailCommandHandler(
            IApplicantRepository applicantRepository,
            IEmailAuthenticatorRepository emailAuthenticatorRepository,
            AuthBusinessRules authBusinessRules)
        {
            _applicantRepository = applicantRepository;
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _authBusinessRules = authBusinessRules;
        }
        public async Task Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            EmailAuthenticator? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(
            predicate: e => e.UserId == request.UserId && e.ActivationKey == request.ActivationCode,
            cancellationToken: cancellationToken);
            await _authBusinessRules.EmailAuthenticatorShouldBeExists(emailAuthenticator);
            await _authBusinessRules.EmailAuthenticatorActivationKeyShouldBeExists(emailAuthenticator!);

            Applicant? applicant = await _applicantRepository.GetAsync(predicate: e => e.Id == request.UserId,
            cancellationToken: cancellationToken);
            await _authBusinessRules.UserShouldBeExistsWhenSelected(applicant);
            applicant!.EmailVerified = true;
            await _applicantRepository.UpdateAsync(applicant);


        }
    }
}