using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using MediatR;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.Register.Employee;

public class EmployeeRegisterCommand : IRequest<RegisteredResponse>
{
    public EmployeeRegisterDto EmployeeRegisterDto { get; set; }
    public string IpAddress { get; set; }

    public EmployeeRegisterCommand()
    {
        EmployeeRegisterDto = null!;
        IpAddress = string.Empty;
    }

    public EmployeeRegisterCommand(EmployeeRegisterDto employeeRegisterDto, string ipAddress)
    {
        EmployeeRegisterDto = employeeRegisterDto;
        IpAddress = ipAddress;
    }

    public class RegisterCommandHandler : IRequestHandler<EmployeeRegisterCommand, RegisteredResponse>
    {
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IEmployeeRepository _employeeRepository;

        public RegisterCommandHandler(
            IAuthService authService,
            AuthBusinessRules authBusinessRules,
            IEmployeeRepository employeeRepository
        )
        {
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _employeeRepository = employeeRepository;
        }

        public async Task<RegisteredResponse> Handle(EmployeeRegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.EmployeeRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                request.EmployeeRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            Domain.Entities.Employee newEmployee =
                new()
                {
                    FirstName = request.EmployeeRegisterDto.FirstName,
                    LastName = request.EmployeeRegisterDto.LastName,
                    NationalIdentity = request.EmployeeRegisterDto.NationalIdentity,
                    DateOfBirth = request.EmployeeRegisterDto.DateOfBirth,
                    Position = request.EmployeeRegisterDto.Position,
                    UserName = request.EmployeeRegisterDto.UserName,
                    Email = request.EmployeeRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };
            Domain.Entities.Employee createdEmployee = await _employeeRepository.AddAsync(newEmployee);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdEmployee);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdEmployee,
                request.IpAddress
            );
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return registeredResponse;
        }
    }
}
