using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.BootcampImages;

public interface IBootcampImageService
{
    Task<List<BootcampImage>> GetList();
    Task<BootcampImage> Get(Guid id);
    Task<BootcampImage> Add(IFormFile file, BootcampImageRequest request);
    Task<BootcampImage> Update(IFormFile file, BootcampImage BootcampImage);
    Task<BootcampImage> Delete(BootcampImage BootcampImage);
    Task<List<BootcampImage>> GetImagesByBootcampId(Guid id);
}
