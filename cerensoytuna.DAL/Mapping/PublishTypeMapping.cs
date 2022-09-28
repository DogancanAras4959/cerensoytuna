using cerensoytuna.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace cerensoytuna.DAL.Mapping
{
    public class PublishTypeMapping : IEntityTypeConfiguration<PublishType>
    {
        public void Configure(EntityTypeBuilder<PublishType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TypeName).HasMaxLength(70).IsRequired();
        }
    }
}
