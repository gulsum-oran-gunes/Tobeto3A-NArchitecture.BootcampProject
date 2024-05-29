using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Dtos;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.Register.Instructor;

public class InstructorRegisterCommand : IRequest<RegisteredResponse>, ICacheRemoverRequest
{
    public InstructorRegisterDto InstructorRegisterDto { get; set; }
    public string IpAddress { get; set; }
    public bool BypassCache { get; }

    public string? CacheKey { get; }

    public string[]? CacheGroupKey => ["GetInstructors"];

    public InstructorRegisterCommand()
    {
        InstructorRegisterDto = null!;
        IpAddress = string.Empty;
    }

    public InstructorRegisterCommand(InstructorRegisterDto instructorRegisterDto, string ipAddress)
    {
        InstructorRegisterDto = instructorRegisterDto;
        IpAddress = ipAddress;
    }

    public class RegisterCommandHandler : IRequestHandler<InstructorRegisterCommand, RegisteredResponse>
    {
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public RegisterCommandHandler(
            IAuthService authService,
            AuthBusinessRules authBusinessRules,
            IInstructorRepository ınstructorRepository,
            IUserOperationClaimRepository userOperationClaimRepository

        )
        {
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _instructorRepository = ınstructorRepository;
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task<RegisteredResponse> Handle(InstructorRegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.InstructorRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                request.InstructorRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            Domain.Entities.Instructor newInstructor =
                new()
                {
                    FirstName = request.InstructorRegisterDto.FirstName,
                    LastName = request.InstructorRegisterDto.LastName,
                    NationalIdentity = request.InstructorRegisterDto.NationalIdentity,
                    DateOfBirth = request.InstructorRegisterDto.DateOfBirth,
                    CompanyName = request.InstructorRegisterDto.CompanyName,
                    UserName = request.InstructorRegisterDto.UserName,
                    Email = request.InstructorRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };
            Domain.Entities.Instructor createdInstructor = await _instructorRepository.AddAsync(newInstructor);
            UserOperationClaim newUserOperationClaim = new() { UserId = createdInstructor.Id, OperationClaimId = 157 };

            await _userOperationClaimRepository.AddAsync(newUserOperationClaim);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdInstructor);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdInstructor,
                request.IpAddress
            );
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return registeredResponse;
        }
    }
}
