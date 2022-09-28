using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cerensoytuna.DAL.Models;

namespace cerensoytuna.DAL.Mapping
{
    public class RolesMapping : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.roleName);
        }
    }
}
