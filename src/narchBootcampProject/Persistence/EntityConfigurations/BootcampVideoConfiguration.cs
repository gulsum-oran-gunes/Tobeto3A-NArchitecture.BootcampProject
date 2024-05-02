using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;
public class BootcampVideoConfiguration : IEntityTypeConfiguration<BootcampVideo>
{
    public void Configure(EntityTypeBuilder<BootcampVideo> builder)
    {
        builder.ToTable("BootcampVideos").HasKey(bi => bi.Id);

        builder.Property(bi => bi.Id).HasColumnName("Id").IsRequired();
        builder.Property(bi => bi.BootcampId).HasColumnName("BootcampId");
        builder.Property(bi => bi.Title).HasColumnName("Title");
        builder.Property(bi => bi.Description).HasColumnName("Description");
        builder.Property(bi => bi.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(bi => bi.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(bi => bi.DeletedDate).HasColumnName("DeletedDate");
        builder.HasOne(x => x.Bootcamp);

        builder.HasQueryFilter(bi => !bi.DeletedDate.HasValue);
    }
}
