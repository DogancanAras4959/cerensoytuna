using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using cerensoytuna.COMMON.SeoDTO;
using cerensoytuna.COMMON.SeoDTO.SeoMetaDto;
using cerensoytuna.DAL.Models;

namespace cerensoytuna.ENGINES.Interface
{
    public interface ISeoService
    {
        Task<int> CreateSeoScore(SeoScoreDto model, string uniqueCode);
        SeoScoreDto GetSeoScoreByPostId(int PostId);
        SeoScoreDto GetSeoScore(int Id);
        Task CreateSeoMetaToSeoScore(int seoScoreId);
        List<SeoMetaListItemDto> listSeoMetasBySeoScoreId(int seoScoreId);
        List<SeoMetaListItemDto> listSeoMetasBySeoScoreIdByAnalyze(int seoScoreId);

        bool UpdateSeoScoreAfterCreateTask(int Id);
        SeoCheckMeta SeoMetaIsDone(int Id);
        SeoCheckMeta SeoMetaIsNotDone(int Id);
        SeoScore DownPointSeoScore(int SeoScoreId, int point);
        SeoScore IncreaseSeoScore(int seoScoreId, int point);
    }
}
