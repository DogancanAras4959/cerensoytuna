using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cerensoytuna.DAL.Core;

namespace cerensoytuna.DAL.Models
{
    [Table("roleUsers")]
    public class RoleUsers : IEntity
    {
        public RoleUsers()
        {

        }
        public int Id { get; set; }

        [ForeignKey("role")]
        public int roleId { get; set; }

        [ForeignKey("user")]
        public int userId { get; set; }

        public Users user { get; set; }
        public Roles role { get; set; }
    }
}
