
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using cerensoytuna.DAL.Core;

namespace cerensoytuna.DAL.Models
{
    [Table("seoscore")]

    public class SeoScore : GeneralModel, IEntity
    {
        public SeoScore()
        {
            seoMetas = new List<SeoCheckMeta>();
        }

        public string UniqeCode { get; set; }
        public string Note { get; set; }
        public int Amount { get; set; }
        public int Level { get; set; }
        public bool IsFinished { get; set; }
        public bool IsCreated { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        public Post Post { get; set; }
        public virtual ICollection<SeoCheckMeta> seoMetas { get; set; }

}
}
