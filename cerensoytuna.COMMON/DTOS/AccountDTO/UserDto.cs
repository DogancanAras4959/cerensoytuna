using System;
using System.Collections.Generic;
using System.Text;
using cerensoytuna.COMMON.DTOS;

namespace cerensoytuna.COMMON.AccountDTO
{
    public class UserDto : BaseDto
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
    }
}
