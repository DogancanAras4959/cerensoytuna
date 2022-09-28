using cerensoytuna.COMMON.DTOS.LangDTO;
using cerensoytuna.CORE.UnitOfWork;
using cerensoytuna.DAL;
using cerensoytuna.DAL.Models;
using cerensoytuna.ENGINE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cerensoytuna.ENGINE.Engines
{
    public class LangService : ILangService
    {
        private readonly IUnitOfWork<cerensoytunadbcontext> _unitOfWork;
        public LangService(IUnitOfWork<cerensoytunadbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<LangListItemDto> langList()
        {
            IEnumerable<Language> langs = _unitOfWork.GetRepository<Language>().Where(x => x.Id > 0, x => x.OrderBy(y => y.Id), "", 1, 30);

            return langs.Select(x => new LangListItemDto
            {
                Id = x.Id,
                langTitle = x.langTitle,

            }).ToList();
        }
    }
}
