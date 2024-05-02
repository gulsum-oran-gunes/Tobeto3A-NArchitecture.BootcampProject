using Application.Services.BootcampImages;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BootcampVideoService;
public interface IBootcampVideoService
{
    Task<List<BootcampVideo>> GetList();
    Task<BootcampVideo> Get(Guid id);
    Task<BootcampVideo> Add(IFormFile videoFile, BootcampVideoRequest request);
    Task<BootcampVideo> Update(IFormFile videoFile, BootcampVideo BootcampVideo);
    Task<BootcampVideo> Delete(BootcampVideo BootcampVideo);
    Task<List<BootcampVideo>> GetVideosByBootcampId(Guid id);
   
}
