using cerensoytuna.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cerensoytuna.DAL.Mapping
{
    public class PostLanguageMapping : IEntityTypeConfiguration<PostLanguage>
    {
        public void Configure(EntityTypeBuilder<PostLanguage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.postTrTitle).HasMaxLength(250);
            builder.Property(x => x.postEngTitle).HasMaxLength(250);
        }
    }
}
