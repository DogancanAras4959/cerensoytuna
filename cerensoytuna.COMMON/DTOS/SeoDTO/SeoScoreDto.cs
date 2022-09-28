using System;
using System.Collections.Generic;
using System.Text;
using cerensoytuna.COMMON.DTOS;

namespace cerensoytuna.COMMON.SeoDTO
{
    public class SeoScoreDto : BaseDto
    {
        public string UniqeCode { get; set; }
        public string Note { get; set; }
        public int Amount { get; set; }
        public int Level { get; set; }
        public int PostId { get; set; }
        public bool IsCreated { get; set; }
        public bool IsFinished { get; set; }
    }
}
