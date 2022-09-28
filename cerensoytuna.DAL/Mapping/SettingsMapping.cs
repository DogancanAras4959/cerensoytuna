﻿using cerensoytuna.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace cerensoytuna.DAL.Mapping
{
    public class SettingsMapping : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Logo).HasMaxLength(100);
            builder.Property(x => x.SiteName).HasMaxLength(100);
            builder.Property(x => x.SiteSlogan).HasMaxLength(100);
            builder.Property(x => x.CopyrightText);
            builder.Property(x => x.CopyrightTextTitle).HasMaxLength(100);
            builder.Property(x => x.FooterLogo).HasMaxLength(100);
        }
    }
}
