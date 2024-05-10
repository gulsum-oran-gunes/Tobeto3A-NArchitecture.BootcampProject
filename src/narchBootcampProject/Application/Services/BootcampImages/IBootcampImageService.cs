using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.BootcampImages;

public interface IBootcampImageService
{
    Task<List<BootcampImage>> GetList();
    Task<BootcampImage> Get(int id);
    Task<BootcampImage> Add(IFormFile file, BootcampImageRequest request);
    Task<BootcampImage> Update(IFormFile file, UpdateBootcampImageRequest request);
    Task<BootcampImage> Delete(BootcampImage request);
    Task<List<BootcampImage>> GetImagesByBootcampId(Guid id);
}
