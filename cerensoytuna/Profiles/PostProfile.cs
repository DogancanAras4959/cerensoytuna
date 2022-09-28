using AutoMapper;
using cerensoytuna.COMMON.DTOS.PostDTO.PostLanguageDTO;
using cerensoytuna.COMMON.PostDTO;
using cerensoytuna.COMMON.PostDTO.TagPostDTO;
using cerensoytuna.COMMON.SetingsDTO;
using cerensoytuna.Models.PostLanguageModel;
using cerensoytuna.Models.PostModel;
using cerensoytuna.Models.SettingsModel;
using cerensoytuna.Models.TagPostModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cerensoytuna.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostListItemDto, PostListViewModel>()
                .ForMember(x => x.publishtype, y => y.MapFrom(t => t.publishtype));

            CreateMap<TagPostListItemDto, TagPostListViewModel>().ForMember(x => x.Post, y => y.MapFrom(t => t.Post)).ForMember(x => x.tag, y => y.MapFrom(t => t.tag));

            CreateMap<PostDto, PostEditViewModel>();
            CreateMap<PostEditViewModel, PostDto>();

            CreateMap<SettingsEditViewModel, SettingsDto>();
            CreateMap<SettingsDto, SettingsEditViewModel>();
            CreateMap<SettingsDto, SettingsViewModel>();

            CreateMap<PostLanguageDto, EditPostLanguageViewModel>();
        }
    }
}
