
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using cerensoytuna.DAL.Core;

namespace cerensoytuna.DAL.Models
{
    [Table("tags")]

    public class Tags : IEntity
    {
        public Tags()
        {
            tagPostForTag = new List<TagPost>();
        }
        public int Id { get; set; }
        public string TagName { get; set; }
        public virtual ICollection<TagPost> tagPostForTag { get; set; }

    }
}
