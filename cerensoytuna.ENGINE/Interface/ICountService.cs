using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cerensoytuna.ENGINES.Interface
{
    public interface ICountService
    {
        int CountPost();
        int CountTags();

        //Task<int> CountAgencyPost();
        int CountUsers();

    }
}
