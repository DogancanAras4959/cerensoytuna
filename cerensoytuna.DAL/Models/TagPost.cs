
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using cerensoytuna.DAL.Core;

namespace cerensoytuna.DAL.Models
{
    [Table("tagPost")]

    public class TagPost : IEntity
    {
        public TagPost()
        {

        }
        public int Id { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        
        [ForeignKey("tag")]
        public int TagId { get; set; }

        public virtual Tags tag { get; set; }
        public virtual Post Post { get; set; }
    }
}
