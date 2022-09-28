using System;
using System.Collections.Generic;
using System.Text;
using cerensoytuna.COMMON.DTOS;
using cerensoytuna.DAL.Models;

namespace cerensoytuna.COMMON.PostDTO.TagPostDTO
{
    public  class TagPostDto : BaseDto
    {
        public int TagId { get; set; }
        public int PostId { get; set; }
        public Tags tag { get; set; }
        public Post Post { get; set; }
    }
}
