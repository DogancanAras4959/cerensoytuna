using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using cerensoytuna.DAL.Core;

namespace cerensoytuna.DAL.Models
{
    [Table("cookiepolicy")]

    public class CookiePolicy : GeneralModel, IEntity
    {
        public CookiePolicy()
        {

        }

        public string Title { get; set; }
        public string Content { get; set; }

        //[ForeignKey("user")]
        //public int UserId { get; set; }
        //public Users user { get; set; }
    }
}
