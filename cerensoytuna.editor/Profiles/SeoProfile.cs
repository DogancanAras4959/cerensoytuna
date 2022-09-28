using AutoMapper;
using cerensoytuna.COMMON.SeoDTO;
using cerensoytuna.COMMON.SeoDTO.SeoMetaDto;
using cerensoytuna.editor.Models.SeoModel;
using cerensoytuna.editor.Models.SeoModel.SeoMetaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cerensoytuna.editor.Profiles
{
    public class SeoProfile : Profile
    {
        public SeoProfile()
        {
            CreateMap<SeoScoreDto, SeoScoreViewModel>();
            CreateMap<SeoScoreCreateViewModel, SeoScoreDto>();
            CreateMap<SeoScoreEditViewModel, SeoScoreDto>();
            CreateMap<SeoScoreDto, SeoScoreEditViewModel>();
            CreateMap<SeoMetaListItemDto, SeoMetaListViewModel>();
            CreateMap<SeoMetaCreateViewModel, SeoMetaDto>();
        }
    }
}
