using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cerensoytuna.DAL.Core;

namespace cerensoytuna.DAL.Models
{
    [Table("roles")]
    public class Roles : GeneralModel, IEntity
    {
        public Roles()
        {
            roleUserList = new List<RoleUsers>();
        }

        public string roleName { get; set; }
        public virtual ICollection<RoleUsers> roleUserList { get; set; }
    }
}
