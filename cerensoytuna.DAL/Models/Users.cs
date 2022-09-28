
using cerensoytuna.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

using cerensoytuna.DAL.Core;

namespace cerensoytuna.DAL.Models
{
    [Table("users")]
    public class Users : GeneralModel, IEntity
    {
        public Users()
        { 
            roleUserList = new List<RoleUsers>();
        }

        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public virtual ICollection<RoleUsers> roleUserList { get; set; }
     
    }
}
