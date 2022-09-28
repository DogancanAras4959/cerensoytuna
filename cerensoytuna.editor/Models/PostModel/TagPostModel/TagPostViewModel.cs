using cerensoytuna.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cerensoytuna.editor.Models.PostModel.TagPostModel
{
    public class TagPostViewModel : BaseViewModel
    {
        public int TagId { get; set; }
        public int NewsId { get; set; }

        public Tags tag { get; set; }
        public Post news { get; set; }
    }
}
