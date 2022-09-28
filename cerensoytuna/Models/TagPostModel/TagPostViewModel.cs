using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cerensoytuna.DAL.Models;

namespace cerensoytuna.Models.TagPostModel
{
    public class TagPostViewModel : BaseViewModel
    {
        public int TagId { get; set; }
        public int PostId { get; set; }

        public Tags tag { get; set; }
        public Post Post { get; set; }
    }
}
