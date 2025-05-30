﻿using cerensoytuna.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cerensoytuna.editor.Models.PostModel
{
    public class PostViewModel : BaseViewModel
    {
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Title { get; set; }
        public string Spot { get; set; }
        public string PostContent { get; set; }
        public int Sorted { get; set; }
        public int LangId { get; set; }
        public int PostId { get; set; }
        public string VideoSlug { get; set; }
        public string Image { get; set; }
        public string Tag { get; set; }
        public DateTime? PublishedTime { get; set; }
        public int PublishTypeId { get; set; }

        public PublishType publishtype { get; set; }
    }
}
