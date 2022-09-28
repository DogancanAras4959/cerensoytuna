

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using cerensoytuna.DAL.Core;

namespace cerensoytuna.DAL.Models
{
    [Table("privacy")]

    public class Privacy : GeneralModel, IEntity
    {
        public Privacy()
        {

        }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
