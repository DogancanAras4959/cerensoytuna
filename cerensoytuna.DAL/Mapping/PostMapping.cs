using cerensoytuna.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace cerensoytuna.DAL.Mapping
{
    public class PostMapping : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Image).HasMaxLength(100).IsRequired();
            builder.Property(x => x.PostContent).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Spot).HasMaxLength(500).IsRequired();
            builder.HasOne(x => x.publishtype).WithMany(x => x.newList).HasForeignKey(x => x.PublishTypeId);
            builder.HasOne(x => x.lang).WithMany(x => x.postList).HasForeignKey(x => x.LangId);
        }
    }
}
