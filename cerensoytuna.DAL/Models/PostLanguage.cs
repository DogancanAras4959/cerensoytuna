using cerensoytuna.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cerensoytuna.DAL.Models
{
    [Table("postlanguage")]
    public class PostLanguage : IEntity
    {
        public PostLanguage()
        {

        }
        public int Id { get; set; }
        public string postTrTitle { get; set; }
        public string postEngTitle { get; set; }

    }
}
