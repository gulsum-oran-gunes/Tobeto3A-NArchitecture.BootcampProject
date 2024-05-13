using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Application.Features.BootcampImages.Commands.Delete;
using Application.Services.BootcampImages;
using Microsoft.AspNetCore.Http;
using Application.Features.InstructorImages.Commands.Delete;

namespace Application.Services.InstructorImages;

public interface IInstructorImageService
{
    Task<InstructorImage?> GetAsync(
        Expression<Func<InstructorImage, bool>> predicate,
        Func<IQueryable<InstructorImage>, IIncludableQueryable<InstructorImage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<InstructorImage>?> GetListAsync(
        Expression<Func<InstructorImage, bool>>? predicate = null,
        Func<IQueryable<InstructorImage>, IOrderedQueryable<InstructorImage>>? orderBy = null,
        Func<IQueryable<InstructorImage>, IIncludableQueryable<InstructorImage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<InstructorImage> AddAsync(IFormFile file, InstructorImageRequest request);
    Task<InstructorImage> UpdateAsync(IFormFile file, UpdateInstructorImageRequest request);
    Task<DeletedInstructorImageResponse> DeleteAsync(int id);

   
}
