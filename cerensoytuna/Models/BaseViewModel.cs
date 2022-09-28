using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cerensoytuna.Models
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
