using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using cerensoytuna.DAL.Core;

namespace cerensoytuna.DAL.Models
{
    [Table("media")]

    public class Media : GeneralModel, IEntity
    {
        public Media()
        {

        }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Extension { get; set; }

        //[ForeignKey("user")]
        //public int userId{ get; set; }
        //public Users user { get; set; }
    }
}
