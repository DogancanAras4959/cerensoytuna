using AutoMapper;
using cerensoytuna.COMMON.DTOS.LangDTO;
using cerensoytuna.COMMON.DTOS.PostDTO.PostLanguageDTO;
using cerensoytuna.COMMON.PostDTO;
using cerensoytuna.COMMON.PostDTO.PublishTypeDTO;
using cerensoytuna.COMMON.PostDTO.TagDTO;
using cerensoytuna.COMMON.PostDTO.TagPostDTO;
using cerensoytuna.editor.Models.LangModel;
using cerensoytuna.editor.Models.PostModel;
using cerensoytuna.editor.Models.PostModel.PostLanguage;
using cerensoytuna.editor.Models.PostModel.PublishTypeModel;
using cerensoytuna.editor.Models.PostModel.TagModel;
using cerensoytuna.editor.Models.PostModel.TagPostModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cerensoytuna.editor.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            #region Haber CreateMap

            CreateMap<PostListItemDto, PostListItemModel>()
                .ForMember(x => x.publishtype, y => y.MapFrom(t => t.publishtype));

            CreateMap<PostCreateViewModel, PostDto>();
            CreateMap<PostEditViewModel, PostDto>();
            CreateMap<PostDto, PostEditViewModel>();

            CreateMap<PublishTypeListItem, PublishTypeListViewModel>();

            CreateMap<TagViewModel, TagDto>();
            CreateMap<CreateTagViewModel, TagDto>();
            CreateMap<TagEditViewModel, TagDto>();
            CreateMap<TagDto, TagEditViewModel>();

            CreateMap<CreatePostLanguageViewModel, PostLanguageDto>();
            CreateMap<PostLanguageDto, EditPostLanguageViewModel>();
            CreateMap<EditPostLanguageViewModel, PostLanguageDto>();

            CreateMap<TagPostViewModel, TagPostDto>();
            CreateMap<TagPostDto, TagPostViewModel>();
            CreateMap<TagPostCreateViewModel, TagPostDto>();
            CreateMap<TagPostEditViewModel, TagPostDto>();
            CreateMap<TagPostDto, TagPostEditViewModel>();

            CreateMap<TagListItemDto, TagListViewModel>();

            CreateMap<TagPostListItemDto, TagPostListViewModel>()
                .ForMember(x => x.news, y => y.MapFrom(t => t.Post))
                .ForMember(x => x.tag, y => y.MapFrom(t => t.tag));

            CreateMap<LangListItemDto, LangListViewModel>();
            CreateMap<LangViewModel, LangDto>();
            CreateMap<LangDto, LangViewModel>();

            #endregion
        }
    }
}
