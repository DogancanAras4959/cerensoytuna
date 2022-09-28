
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cerensoytuna.CORE.UnitOfWork;
using cerensoytuna.DAL;
using cerensoytuna.DAL.Models;
using cerensoytuna.ENGINES.Interface;

namespace cerensoytuna.ENGINES.Engines
{
    public class CountService : ICountService
    {
        private readonly IUnitOfWork<cerensoytunadbcontext> _unitOfWork;
        public CountService(IUnitOfWork<cerensoytunadbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;       
        }
  
        public int CountPost()
        {
            IEnumerable<Post> PostCount = _unitOfWork.GetRepository<Post>().Where(null, x => x.OrderBy(y => y.Id), "", null, null);

            int count = PostCount.Count();

            return count;
        }

        public int CountTags()
        {
            IEnumerable<Tags> tagsCount = _unitOfWork.GetRepository<Tags>().Where(null, x => x.OrderBy(y => y.Id), "", null, null);

            int count = tagsCount.Count();

            return count;
        }

        public int CountUsers()
        {
            IEnumerable<Users> usersCount = _unitOfWork.GetRepository<Users>().Where(null, x => x.OrderBy(y => y.Id), "", null, null);

            int count = usersCount.Count();

            return count;
        }
    }
}
