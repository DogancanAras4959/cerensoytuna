using cerensoytuna.COMMON.DTOS.LangDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cerensoytuna.ENGINE.Interface
{
    public interface ILangService
    {
        List<LangListItemDto> langList();
    }
}
