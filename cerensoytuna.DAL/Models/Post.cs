

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using cerensoytuna.DAL.Core;

namespace cerensoytuna.DAL.Models
{
    [Table("Post")]

    public class Post : GeneralModel, IEntity
    {
        public Post()
        {
            tagPostListForPost = new List<TagPost>();
            seoScorePost = new List<SeoScore>();
        }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Title { get; set; }
        public string Spot { get; set; }
        public string PostContent { get; set; }
        public int Sorted { get; set; }
        public string Image { get; set; }
        public string VideoSlug { get; set; }
        public DateTime? PublishedTime { get; set; }
        public bool IsCommentActive { get; set; }

        [ForeignKey("lang")]
        public int LangId { get; set; }
        public int ParentPostId { get; set; }

        [ForeignKey("publishtype")]
        public int PublishTypeId { get; set; }

        public Language lang { get; set; }
        public virtual PublishType publishtype { get; set; }
        public virtual ICollection<TagPost> tagPostListForPost { get; set; }
        public virtual ICollection<SeoScore> seoScorePost { get; set; }
    }
}