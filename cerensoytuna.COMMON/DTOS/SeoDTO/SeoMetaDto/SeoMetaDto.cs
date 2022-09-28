using System;
using System.Collections.Generic;
using System.Text;
using cerensoytuna.DAL.Models;

namespace cerensoytuna.COMMON.SeoDTO.SeoMetaDto
{
    public class SeoMetaDto
    {
        public int Id { get; set; }
        public string TypeLevel { get; set; }
        public string Requirement { get; set; }
        public int Point { get; set; }
        public int SeoScoreId { get; set; }
        public string metaCode { get; set; }

        public bool IsDone { get; set; }
        public SeoScore seoScore { get; set; }
    }
}
