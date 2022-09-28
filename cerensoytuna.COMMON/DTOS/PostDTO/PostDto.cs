using System;
using System.Collections.Generic;
using System.Text;
using cerensoytuna.COMMON.DTOS;
using cerensoytuna.DAL.Models;

namespace cerensoytuna.COMMON.PostDTO
{
    public class PostDto : BaseDto
    {
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Title { get; set; }
        public string Spot { get; set; }
        public string PostContent { get; set; }
        public int LangId { get; set; }
        public string VideoSlug { get; set; }
        public string Image { get; set; }
        public int Sorted { get; set; }
        public DateTime? PublishedTime { get; set; }
        public bool IsCommentActive { get; set; }
        public int PublishTypeId { get; set; }

        public PublishType publishtype { get; set; }
    }
}
