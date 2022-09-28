using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cerensoytuna.editor.Models.UserModel
{
    public class UserViewModel : BaseViewModel
    {
        public string ProfileImage { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string rePassword { get; set; }
        public string UserName { get; set; }
    }
}
