
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class BootcampContent : Entity <int>

{
    public int BootcampId { get; set; }
    public string? VideoUrl { get; set; }
    public string? Content { get; set; }
    public virtual Bootcamp? Bootcamp { get; set; }
    public ICollection<ApplicantBootcampContent> ApplicantBootcampContents { get; set; }

    public BootcampContent()
    {
        ApplicantBootcampContents = new HashSet<ApplicantBootcampContent>();
    }

    public BootcampContent(int bootcampId, string? videoUrl, string? content)
    {
        BootcampId = bootcampId;
        VideoUrl = videoUrl;
        Content = content;
    }
}
