using cerensoytuna.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using cerensoytuna.DAL.Core;

namespace cerensoytuna.DAL.Models
{
    [Table("aboutus")]

    public class AboutUs : GeneralModel, IEntity
    {
        public AboutUs()
        {

        }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
