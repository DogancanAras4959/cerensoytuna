using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using cerensoytuna.COMMON.DTOS.PostDTO.PostLanguageDTO;
using cerensoytuna.COMMON.PostDTO;
using cerensoytuna.COMMON.PostDTO.MediaDTO;
using cerensoytuna.COMMON.PostDTO.PublishTypeDTO;
using cerensoytuna.COMMON.PostDTO.TagDTO;
using cerensoytuna.COMMON.PostDTO.TagPostDTO;
using cerensoytuna.DAL.Models;

namespace cerensoytuna.ENGINES.Interface
{
    public interface IPostService
    {

        #region Haber
        List<PostListItemDto> PostList();
        List<PostListItemDto> PostListWithWeb();
        List<PostListItemDto> PostListJsonData();
        List<PublishTypeListItem> publishTypeList();
        List<PostListItemDto> searchDataInPost(string searchName);
        List<PostListItemDto> PostListLoadByScroll(int pageIndex, int pageSize);
        Task<bool> PostIfExists(string title);
        Task<int> createPost(PostDto model);
        Task<int> editPost(PostDto model);
        Task<int> editSortedPost(int itemId, int count);
        PostDto getPost(int id);
        PostDto getPostToTitle(string Title);
        bool PostDelete(int id);
        Task<bool> ChangeSorted(int id, int sira);
        Task<bool> IsActiveEnabled(int id);

        #endregion

        #region Etiket

        Task<bool> createTag(TagDto model);
        TagDto getTags(string name);
        bool tagDelete(int id);
        Task InsertTagToProduct(string v, int resultId);
        List<TagPostListItemDto> tagsListWithPost();
        List<TagPostListItemDto> tagsListWithPostWeb();
        List<TagPostListItemDto> tagsListWithPostById(int etiketId);
        List<TagPostListItemDto> tagsListWithPostWebSearch(string search);
        TagDto tagGet(int etiketId);
        List<TagPostListItemDto> tagsListWithPostByPostId(int id); 
        List<TagPostListItemDto> tagsListWithPostByTagId(int? id);
        List<TagListItemDto> tagList();
        List<PostListItemDto> PostListWithLastOneToFive();
        List<TagListItemDto> tagListWithSearch(string searchName);
        #endregion

        #region Videos
    
        List<MediaListItemDto> mediaList();
        Task<bool> insertMedia(MediaDto model);

        #endregion

        #region PostLanguageList 
        List<PostListItemDto> PostListWithWebDeu();
        List<PostListItemDto> PostListWithWebEng();
        List<PostListItemDto> PostListWithWebTr();
        PostLanguageDto getPostLanguage(string title);
        Task<int> insertLanguageSwitch(PostLanguageDto model);
        Task<int> editPostLanguageTr(PostLanguageDto model);
        Task<int> editPostLanguageEn(PostLanguageDto model);
        Task<int> editPostLanguageDeu(PostLanguageDto model);


        #endregion

    }
}
