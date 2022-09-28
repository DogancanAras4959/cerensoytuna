using cerensoytuna.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace cerensoytuna.DAL.Mapping
{
    public class SeoScoreMapping : IEntityTypeConfiguration<SeoScore>
    {
        public void Configure(EntityTypeBuilder<SeoScore> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Note);
            builder.Property(x => x.UniqeCode);
            builder.HasOne(x => x.Post).WithMany(x => x.seoScorePost).HasForeignKey(x => x.PostId);
        }
    }
}
