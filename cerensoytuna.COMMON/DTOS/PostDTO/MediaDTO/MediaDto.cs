using System;
using System.Collections.Generic;
using System.Text;
using cerensoytuna.COMMON.DTOS;

namespace cerensoytuna.COMMON.PostDTO.MediaDTO
{
    public class MediaDto : BaseDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Extension { get; set; }
    }
}
