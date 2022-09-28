using cerensoytuna.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace cerensoytuna.DAL.Mapping
{
    public class TagMapping : IEntityTypeConfiguration<Tags>
    {
        public void Configure(EntityTypeBuilder<Tags> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TagName).HasMaxLength(70);
        }
    }
}
