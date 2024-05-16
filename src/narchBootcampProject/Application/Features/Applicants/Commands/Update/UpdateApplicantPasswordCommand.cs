using Application.Features.Users.Commands.UpdateFromAuth;
using Application.Features.Users.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Applicants.Commands.Update;
public class UpdateApplicantPasswordCommand : IRequest<UpdateApplicantPasswordResponse>
{
    public Guid Id { get; set; }
    public string Password { get; set; }
    public string? NewPassword { get; set; }

    public UpdateApplicantPasswordCommand(Guid id, string password)
    {
        Id = id;
        Password = password;
    }

    public class UpdateApplicantPasswordCommandHandler : IRequestHandler<UpdateApplicantPasswordCommand, UpdateApplicantPasswordResponse>
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IAuthService _authService;

        public UpdateApplicantPasswordCommandHandler(IApplicantRepository applicantRepository, IMapper mapper, UserBusinessRules userBusinessRules, IAuthService authService)
        {
            _applicantRepository = applicantRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
            _authService = authService;
        }

        public async Task<UpdateApplicantPasswordResponse> Handle(
           UpdateApplicantPasswordCommand request,
           CancellationToken cancellationToken
       )
        {
            Applicant? applicant = await _applicantRepository.GetAsync(
                predicate: u => u.Id.Equals(request.Id),
                cancellationToken: cancellationToken
            );
            await _userBusinessRules.UserShouldBeExistsWhenSelected(applicant);
            await _userBusinessRules.UserPasswordShouldBeMatched(user: applicant!, request.Password);
           

            applicant = _mapper.Map(request, applicant);
            if (request.NewPassword != null && !string.IsNullOrWhiteSpace(request.NewPassword))
            {
                HashingHelper.CreatePasswordHash(
                    request.NewPassword,
                    passwordHash: out byte[] passwordHash,
                    passwordSalt: out byte[] passwordSalt
                );
                applicant!.PasswordHash = passwordHash;
                applicant!.PasswordSalt = passwordSalt;
            }

             await _applicantRepository.UpdateAsync(applicant!);

            UpdateApplicantPasswordResponse response = _mapper.Map<UpdateApplicantPasswordResponse>(applicant);
            response.AccessToken = await _authService.CreateAccessToken(applicant!);
            return response;
        }




    }

}
