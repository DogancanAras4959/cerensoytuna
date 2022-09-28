
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using cerensoytuna.DAL.Core;

namespace cerensoytuna.DAL.Models
{
    [Table("publishtype")]

    public class PublishType : GeneralModel, IEntity
    {
        public PublishType()
        {
            newList = new List<Post>();
        }

        public string TypeName { get; set; }
     
        public virtual ICollection<Post> newList { get; set; }
    }
}
