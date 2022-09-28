using cerensoytuna.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace cerensoytuna.DAL.Mapping
{
    public class StreamMapping : IEntityTypeConfiguration<StreamPolicy>
    {
        public void Configure(EntityTypeBuilder<StreamPolicy> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Content).IsRequired();
        }
    }
}
