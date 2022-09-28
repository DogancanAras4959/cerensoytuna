using cerensoytuna.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cerensoytuna.DAL.Models
{
    [Table("language")]

    public class Language : GeneralModel, IEntity 
    {
        public Language()
        {
            postList = new List<Post>();
        }

        public string langTitle { get; set; }

        public List<Post> postList { get; set; }

    }
}
