using Application.Features.Employees.Constants;
using Application.Features.Employees.Constants;
using Application.Features.Employees.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Application.Services.UserOperationClaims;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.Employees.Constants.EmployeesOperationClaims;

namespace Application.Features.Employees.Commands.Delete;

public class DeleteEmployeeCommand
    : IRequest<DeletedEmployeeResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, EmployeesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetEmployees"];

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, DeletedEmployeeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeBusinessRules _employeeBusinessRules;
        private readonly IAuthService _authService;
        private readonly IUserOperationClaimService _userOperationClaimService;

        public DeleteEmployeeCommandHandler(
            IMapper mapper,
            IEmployeeRepository employeeRepository,
            EmployeeBusinessRules employeeBusinessRules,
            IAuthService authService,
            IUserOperationClaimService userOperationClaimService)
            
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _employeeBusinessRules = employeeBusinessRules;
           _authService = authService;
            _userOperationClaimService = userOperationClaimService;
        }

        public async Task<DeletedEmployeeResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee? employee = await _employeeRepository.GetAsync(
                predicate: e => e.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _employeeBusinessRules.EmployeeShouldExistWhenSelected(employee);
            //await _authService.DeleteAllRefreshTokensByUserIdAsync(employee.Id);
            //await _userOperationClaimService.DeleteAllUserOperationClaimByUserIdAsync(employee.Id);
            await _employeeRepository.DeleteAsync(employee!);

            DeletedEmployeeResponse response = _mapper.Map<DeletedEmployeeResponse>(employee);
            return response;
        }
    }
}
