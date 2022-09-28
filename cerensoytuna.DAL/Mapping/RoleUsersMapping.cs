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
    public class RoleUserMapping : IEntityTypeConfiguration<RoleUsers>
    {
        public void Configure(EntityTypeBuilder<RoleUsers> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.role).WithMany(x => x.roleUserList).HasForeignKey(x => x.roleId);
            builder.HasOne(x => x.user).WithMany(x => x.roleUserList).HasForeignKey(x => x.userId);
        }
    }
}
