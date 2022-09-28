using cerensoytuna.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cerensoytuna.COMMON.DTOS.PostDTO.PostLanguageDTO
{
    public class PostLanguageDto : BaseDto
    {
        public string PostEngTitle { get; set; }
        public string PostTrTitle { get; set; }
    }
}
