using cerensoytuna.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cerensoytuna.editor.Models.SeoModel
{
    public class SeoScoreViewModel : BaseViewModel
    {
        public int PostId { get; set; }
        public string Note { get; set; }
        public int Amount { get; set; }
        public int Level { get; set; }
        public bool IsFinished { get; set; }
        public string UniqueCode { get; set; }
        public bool IsCreated { get; set; }

        public Post newsToSeo { get; set; }
    }
}
