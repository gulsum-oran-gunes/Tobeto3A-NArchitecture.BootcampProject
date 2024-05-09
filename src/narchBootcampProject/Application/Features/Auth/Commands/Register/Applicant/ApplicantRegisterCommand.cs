using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Application.Services.UserOperationClaims;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.Register.Applicant;

public class ApplicantRegisterCommand : IRequest<RegisteredResponse>, ICacheRemoverRequest
{
    public ApplicantRegisterDto ApplicantRegisterDto { get; set; }
    public string IpAddress { get; set; }

    public bool BypassCache { get; }

    public string? CacheKey { get; }

    public string[]? CacheGroupKey => ["GetApplicants"];
  

    public ApplicantRegisterCommand()
    {
        ApplicantRegisterDto = null!;
        IpAddress = string.Empty;
    }

    public ApplicantRegisterCommand(ApplicantRegisterDto applicantRegisterDto, string ipAddress)
    {
        ApplicantRegisterDto = applicantRegisterDto;
        IpAddress = ipAddress;
    }

    public class ApplicantRegisterCommandHandler : IRequestHandler<ApplicantRegisterCommand, RegisteredResponse>
    {
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IApplicantRepository _applicantRepository;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public ApplicantRegisterCommandHandler(
            IAuthService authService,
            AuthBusinessRules authBusinessRules,
            IApplicantRepository applicantRepository,
            IUserOperationClaimRepository userOperationClaimRepository
        )
        {
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _applicantRepository = applicantRepository;
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task<RegisteredResponse> Handle(ApplicantRegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.ApplicantRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                request.ApplicantRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            Domain.Entities.Applicant newApplicant =
                new()
                {
                    FirstName = request.ApplicantRegisterDto.FirstName,
                    LastName = request.ApplicantRegisterDto.LastName,
                  
                    UserName = request.ApplicantRegisterDto.UserName,
                    Email = request.ApplicantRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };
            Domain.Entities.Applicant createdApplicant = await _applicantRepository.AddAsync(newApplicant);
            UserOperationClaim newUserOperationClaim = new() { UserId = createdApplicant.Id, OperationClaimId = 114 };

            await _userOperationClaimRepository.AddAsync(newUserOperationClaim);
            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdApplicant);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdApplicant,
                request.IpAddress
            );
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return registeredResponse;
        }
    }
}
