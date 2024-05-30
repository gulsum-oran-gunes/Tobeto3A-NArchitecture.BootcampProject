using Application.Features.Certificates.Constants;
using Application.Features.Certificates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using static Application.Features.Certificates.Constants.CertificatesOperationClaims;

namespace Application.Features.Certificates.Commands.Create;

public class CertificateDoc : IDocument
{
   
    private readonly Certificate _certificate;

    public CertificateDoc( Certificate certificate)
    {
      
        _certificate = certificate;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                // background image size
                page.Size(1678, 1182);
                page.Background()
                    .Image("../Application/Features/Certificates/Resources/Assets/Cert_Template.png");

                page.Content().Column(column =>
                {
                    var baseHeight = 1182 / 3;

                    column.Spacing(30);

                    column.Item()
                        .Height(baseHeight + 100)
                        .AlignCenter()
                        .AlignBottom()
                        .TranslateX(100)
                        .Text(_certificate.Bootcamp.Name)
                        .FontColor("#000000").FontSize(48);

                    column.Item()
                        .Height(baseHeight - 100)
                        .AlignCenter()
                        .AlignTop()
                        .TranslateX(100)
                        .TranslateY(-50)
                        .Text(_certificate.Applicant.FirstName.ToUpper() + " " + _certificate.Applicant.LastName.ToUpper())
                        .Italic()
                        .LineHeight(1.5f)
                        .Bold()
                        .FontColor("#000000").FontSize(112);

                    column.Item()
                        .Height(baseHeight / 2 - 50)
                        .AlignCenter()
                        .AlignMiddle()
                        .TranslateX(-250)
                        .Text(DateOnly.FromDateTime(_certificate.CreatedDate).ToString("dd/MM/yyyy"))
                        .FontColor("#000000").FontSize(36);
                });


            });

    }

}

public class CreateCertificateCommand : IRequest<CreatedCertificateResponse>, /*ISecuredRequest,*/ ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid ApplicantId { get; set; }
    public int BootcampId { get; set; }

    //public string[] Roles => [Admin, Write, CertificatesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCertificates"];

    public class CreateCertificateCommandHandler : IRequestHandler<CreateCertificateCommand, CreatedCertificateResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICertificateRepository _certificateRepository;
        private readonly IApplicantRepository _applicantRepository;
        private readonly IBootcampRepository _bootcampRepository;
        private readonly CertificateBusinessRules _certificateBusinessRules;

        public CreateCertificateCommandHandler(IMapper mapper, ICertificateRepository certificateRepository,
                                         IApplicantRepository applicantRepository, IBootcampRepository bootcampRepository,
                                         CertificateBusinessRules certificateBusinessRules)
        {
            _mapper = mapper;
            _certificateRepository = certificateRepository;
            _applicantRepository = applicantRepository;
            _bootcampRepository = bootcampRepository;
            _certificateBusinessRules = certificateBusinessRules;
        }

        public async Task<CreatedCertificateResponse> Handle(CreateCertificateCommand request, CancellationToken cancellationToken)
        {

            var certificate = await _certificateRepository.GetAsync(
                predicate: c => c.ApplicantId == request.ApplicantId && c.BootcampId == request.BootcampId,
                include: x => x.Include(x => x.Applicant).Include(x => x.Bootcamp),
                cancellationToken: cancellationToken
            );
            if (certificate == null)
            {
                await _certificateRepository.AddAsync(_mapper.Map<Certificate>(request));
                certificate = await _certificateRepository.GetAsync(
                    predicate: c => c.ApplicantId == request.ApplicantId && c.BootcampId == request.BootcampId,
                    include: x => x.Include(x => x.Applicant).Include(x => x.Bootcamp),
                    cancellationToken: cancellationToken
                );
            }

            CreatedCertificateResponse response = _mapper.Map<CreatedCertificateResponse>(certificate);

            var tempPath = System.IO.Path.GetTempPath();
            var filePath = tempPath + certificate.BootcampId + "_" + certificate.ApplicantId + "_certificate.pdf";

            var pdf = new CertificateDoc(certificate);
            pdf.GeneratePdf(filePath);

            response.File = File.ReadAllBytes(filePath);
            File.Delete(filePath);

            return response;
        }


    }
}