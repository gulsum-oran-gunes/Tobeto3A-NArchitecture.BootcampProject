using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Application.Services.UserOperationClaims;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using MimeKit;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Mailing;
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
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;

        public ApplicantRegisterCommandHandler(
            IAuthService authService,
            AuthBusinessRules authBusinessRules,
            IApplicantRepository applicantRepository,
            IUserOperationClaimRepository userOperationClaimRepository,
            IEmailAuthenticatorRepository emailAuthenticatorRepository,
            IMailService mailService,
            IAuthenticatorService authenticatorService,
            IConfiguration configuration
        )
        {
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _applicantRepository = applicantRepository;
            _userOperationClaimRepository = userOperationClaimRepository;
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _mailService = mailService;
            _authenticatorService = authenticatorService;
            _configuration = configuration;
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

            EmailAuthenticator emailAuthenticator = await _authenticatorService.CreateEmailAuthenticator(createdApplicant);
            EmailAuthenticator addedEmailAuthenticator = await _emailAuthenticatorRepository.AddAsync(emailAuthenticator);
            
            var frontend = _configuration.GetValue<string>("FrontendAddress");
            var toEmailList = new List<MailboxAddress> { new(name: createdApplicant.Email, createdApplicant.Email) };
            var verifyUrl = $"{frontend}/verify?ActivationKey={HttpUtility.UrlEncode(addedEmailAuthenticator.ActivationKey)}";
            
            _mailService.SendMail(new Mail { ToList = toEmailList,
             Subject = "TechItEasy — Verify Your Email",
             TextBody = $"Hesabınızı doğrulamak için şu linke tıklayın: ${verifyUrl}",
                HtmlBody = $@"
                    <html>
                        <body>
                            <div class='container'>
                                <h1>TechItEasy</h1>
                                <p>Hesabınızı doğrulamak için aşağıdaki linke tıklayın:</p>
                               <a href ='{verifyUrl}'>Hesabımı Doğrula</a>                                
                                <p>Eğer linke tıklamakta sorun yaşıyorsanız, aşağıdaki bağlantıyı tarayıcınızın adres çubuğuna yapıştırabilirsiniz:</p>
                               <p>{verifyUrl}</p>
                                <p>Teşekkürler,<br>
                                    TechItEasy Ekibi
                                </p>
                               <p>Bu bir otomatik e-postadır, lütfen yanıtlamayınız.</p>
                            </div>
                        </body>
                    </html>"
                }
                        );

                        RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return registeredResponse;
        }
    }
}
