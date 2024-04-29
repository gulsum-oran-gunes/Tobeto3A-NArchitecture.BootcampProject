using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;


public class BootcampVideo : Entity<int>
{
    public int BootcampId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ThumbnailUrl { get; set; }

    public virtual Bootcamp? Bootcamp { get; set; }

    public BootcampVideo() { }

    public BootcampVideo(int id, int bootcampId, string title, string description, string thumbnailUrl)
        : this()
    {
        Id = id;
        BootcampId = bootcampId;
        Title = title;
        Description = description;
        ThumbnailUrl = thumbnailUrl;

    }
}

