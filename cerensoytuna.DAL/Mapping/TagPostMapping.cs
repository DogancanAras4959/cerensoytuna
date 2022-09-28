using cerensoytuna.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace cerensoytuna.DAL.Mapping
{
    public class TagPostMapping : IEntityTypeConfiguration<TagPost>
    {
        public void Configure(EntityTypeBuilder<TagPost> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Post).WithMany(x => x.tagPostListForPost).HasForeignKey(x => x.PostId);
            builder.HasOne(x => x.tag).WithMany(x => x.tagPostForTag).HasForeignKey(x => x.TagId);
        }
    }
}
