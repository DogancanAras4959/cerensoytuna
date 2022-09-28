
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cerensoytuna.COMMON.DTOS.PostDTO.PostLanguageDTO;
using cerensoytuna.COMMON.PostDTO;
using cerensoytuna.COMMON.PostDTO.MediaDTO;
using cerensoytuna.COMMON.PostDTO.PublishTypeDTO;
using cerensoytuna.COMMON.PostDTO.TagDTO;
using cerensoytuna.COMMON.PostDTO.TagPostDTO;
using cerensoytuna.CORE.UnitOfWork;
using cerensoytuna.DAL;
using cerensoytuna.DAL.Models;
using cerensoytuna.ENGINES.Interface;

namespace cerensoytuna.ENGINES.Engines
{
    public class PostService : IPostService
    {

        private readonly IUnitOfWork<cerensoytunadbcontext> _unitOfWork;
        public PostService(IUnitOfWork<cerensoytunadbcontext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Tag & Tag-Post
        public async Task<bool> createTag(TagDto model)
        {
            Tags newTags = await _unitOfWork.GetRepository<Tags>().AddAsync(new Tags
            {
                TagName = model.TagName
            });

            return newTags != null && newTags.Id != 0;
        }
        public TagDto getTags(string name)
        {
            Tags getTags = _unitOfWork.GetRepository<Tags>().FindAsync(x => x.TagName == name).Result;

            return new TagDto
            {
                Id = getTags.Id,
                TagName = getTags.TagName
            };
        }
        public async Task InsertTagToProduct(string tag, int resultId)
        {
            try
            {
                Post getPost = await _unitOfWork.GetRepository<Post>().FindAsync(x => x.Id == resultId);
                string[] listTags = tag.Split(',');

                for (int i = 0; i < listTags.Count(); i++)
                {
                    if (await _unitOfWork.GetRepository<Tags>().FindAsync(x => x.TagName == listTags[i].Trim().ToString()) != null)
                    {
                        //var ise oluşturmayacak
                    }
                    else
                    {
                        Tags tags = await _unitOfWork.GetRepository<Tags>().AddAsync(new Tags
                        {
                            TagName = listTags[i].Trim().ToString()
                        });
                    }
                }

                foreach (string item in listTags) //Çalışmıyor
                {
                    string etiketAdi = item.Trim();
                    Tags etiketiGetir = await _unitOfWork.GetRepository<Tags>().FindAsync(x => x.TagName == etiketAdi);

                    if (await _unitOfWork.GetRepository<TagPost>().FindAsync(x => x.PostId == getPost.Id && x.TagId == etiketiGetir.Id) != null)
                    {
                        //var ise eklemeyecek
                    }
                    else
                    {
                        TagPost tagPost = await _unitOfWork.GetRepository<TagPost>().AddAsync(new TagPost
                        {
                            PostId = getPost.Id,
                            TagId = etiketiGetir.Id,
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<TagPostListItemDto> tagsListWithPost()
        {
            IEnumerable<TagPost> PostList = _unitOfWork.GetRepository<TagPost>().Where(null, x => x.OrderByDescending(y => y.Id), "tag,Post", 1, 50);

            if (PostList != null)
            {
                return PostList.Select(x => new TagPostListItemDto
                {

                    Id = x.Id,
                    PostId = x.PostId,
                    TagId = x.TagId,
                    Post = x.Post,
                    tag = x.tag,

                }).ToList();
            }
            else
            {
                return null;
            }
        }
        public List<TagPostListItemDto> tagsListWithPostWeb()
        {
            IEnumerable<TagPost> PostList = _unitOfWork.GetRepository<TagPost>().Where(null, x => x.OrderByDescending(y => y.Id), "tag,Post", null, null);

            if (PostList != null)
            {
                return PostList.Select(x => new TagPostListItemDto
                {

                    Id = x.Id,
                    PostId = x.PostId,
                    TagId = x.TagId,
                    Post = x.Post,
                    tag = x.tag,

                }).ToList();
            }
            else
            {
                return null;
            }
        }
        public List<TagPostListItemDto> tagsListWithPostById(int etiketId)
        {
            IEnumerable<TagPost> PostList = _unitOfWork.GetRepository<TagPost>().Where(x => x.TagId == etiketId, x => x.OrderByDescending(y => y.Id), "tag,Post", 1, 50);

            if (PostList != null)
            {
                return PostList.Select(x => new TagPostListItemDto
                {

                    Id = x.Id,
                    PostId = x.PostId,
                    TagId = x.TagId,
                    Post = x.Post,
                    tag = x.tag,

                }).ToList();
            }
            else
            {
                return null;
            }
        }
        public TagDto tagGet(int etiketId)
        {
            Tags getTags = _unitOfWork.GetRepository<Tags>().FindAsync(x => x.Id == etiketId).Result;

            return new TagDto
            {
                Id = getTags.Id,
                TagName = getTags.TagName
            };
        }
        public List<TagPostListItemDto> tagsListWithPostByPostId(int id)
        {
            IEnumerable<TagPost> PostList = _unitOfWork.GetRepository<TagPost>().Where(x => x.PostId == id, x => x.OrderByDescending(y => y.Id), "tag,Post", 1, 50);

            if (PostList != null)
            {
                return PostList.Select(x => new TagPostListItemDto
                {

                    Id = x.Id,
                    PostId = x.PostId,
                    TagId = x.TagId,
                    Post = x.Post,
                    tag = x.tag,

                }).ToList();
            }
            else
            {
                return null;
            }
        }
        public List<TagListItemDto> tagList()
        {
            IEnumerable<Tags> PostList = _unitOfWork.GetRepository<Tags>().Where(null, x => x.OrderByDescending(y => y.Id), "", null, null);

            if (PostList != null)
            {

                return PostList.Select(x => new TagListItemDto
                {
                    Id = x.Id,
                    TagName = x.TagName,

                }).ToList();
            }
            else
            {
                return null;
            }
        }
        public bool tagDelete(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<Tags>().DeleteAsync(new Tags { Id = id });
            return Convert.ToBoolean(result.Result);
        }
        public List<TagPostListItemDto> tagsListWithPostWebSearch(string search)
        {
            IEnumerable<TagPost> getTags = _unitOfWork.GetRepository<TagPost>().Where(null, x => x.OrderByDescending(y => y.Id), "tag,Post", null, null);

            if (!String.IsNullOrEmpty(search))
            {
                getTags = getTags.Where(x => x.tag.TagName!.Contains(search));
            }

            return getTags.Select(x => new TagPostListItemDto
            {
                Id = x.Id,
                tag = x.tag,
                Post = x.Post

            }).ToList();
        }
        public List<TagPostListItemDto> tagsListWithPostByTagId(int? id)
        {
            IEnumerable<TagPost> PostList = _unitOfWork.GetRepository<TagPost>().Where(x => x.TagId == id, x => x.OrderByDescending(y => y.Id), "tag,Post", null, null);

            if (PostList != null)
            {
                return PostList.Select(x => new TagPostListItemDto
                {

                    Id = x.Id,
                    PostId = x.PostId,
                    TagId = x.TagId,
                    Post = x.Post,
                    tag = x.tag,

                }).ToList();
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Media
        public List<MediaListItemDto> mediaList()
        {
            IEnumerable<Media> PostList = _unitOfWork.GetRepository<Media>().Where(null, x => x.OrderBy(y => y.Id), "", null, null);

            if (PostList != null)
            {
                return PostList.Select(x => new MediaListItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    CreatedTime = x.CreatedTime,
                    Extension = x.Extension,
                    UpdatedTime = x.UpdatedTime,
                    Slug = x.Slug
                }).ToList();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> insertMedia(MediaDto model)
        {
            Media newMedia = await _unitOfWork.GetRepository<Media>().AddAsync(new Media
            {
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Extension = model.Extension,
                Slug = model.Slug,
                Title = model.Title,

            });

            return true;
        }

        #endregion

        #region Post
        public List<PostListItemDto> PostList()
        {
            IEnumerable<Post> PostList = _unitOfWork.GetRepository<Post>().Where(null, x => x.OrderByDescending(y => y.Id), "publishtype", null, null);

            if (PostList != null)
            {
                return PostList.Select(x => new PostListItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Spot = x.Spot,
                    Image = x.Image,
                    PostContent = x.PostContent,
                    IsActive = x.IsActive,
                    VideoSlug = x.VideoSlug,
                    LangId = x.LangId,
                    UpdatedTime = x.UpdatedTime,
                    CreatedTime = x.CreatedTime,
                    MetaDescription = x.MetaDescription,
                    PublishTypeId = x.PublishTypeId,
                    PublishedTime = x.PublishedTime,
                    IsCommentActive = x.IsCommentActive,
                    Sorted = x.Sorted,
                    publishtype = x.publishtype,
        
                }).ToList();
            }
            else
            {
                return null;
            }
        }
        public List<PublishTypeListItem> publishTypeList()
        {
            IEnumerable<PublishType> listTypes = _unitOfWork.GetRepository<PublishType>().Where(null, x => x.OrderByDescending(y => y.Id), "", 1, 50);

            return listTypes.Select(x => new PublishTypeListItem
            {
                Id = x.Id,
                TypeName = x.TypeName,

            }).ToList();
        }
        public async Task<bool> PostIfExists(string title) =>
            await _unitOfWork.GetRepository<Post>().FindAsync(x => x.Title == title) != null;
        public async Task<int> createPost(PostDto model)
        {
            try
            {
              

                Post createPost = await _unitOfWork.GetRepository<Post>().AddAsync(new Post
                {
                    Title = model.Title,
                    Spot = model.Spot,
                    IsActive = true,   
                    MetaDescription = model.MetaDescription,
                    VideoSlug = model.VideoSlug,
                    IsCommentActive = model.IsCommentActive,
                    UpdatedTime = DateTime.Now,
                    MetaTitle = model.MetaTitle,
                    CreatedTime = DateTime.Now,
                    PublishTypeId = model.PublishTypeId,
                    PostContent = model.PostContent,
                    LangId = model.LangId,
                    Image = model.Image,
                    Sorted = 0,
                });

                return createPost.Id;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public PostDto getPost(int id)
        {
            //Post getPost = _unitOfWork.GetRepository<Post>().FindAsync(x => x.Id == id).Result;

            Post getPost = _unitOfWork.GetRepository<Post>().Where(x => x.Id == id, x => x.OrderBy(x => x.UpdatedTime), "publishtype", null, null).SingleOrDefault();

            if (getPost != null)
            {
                return new PostDto
                {
                    Id = getPost.Id,
                    Title = getPost.Title,
                    Spot = getPost.Spot,
                    IsActive = getPost.IsActive,
                    VideoSlug = getPost.VideoSlug,
                    MetaDescription = getPost.MetaDescription,
                    IsCommentActive = getPost.IsCommentActive,
                    UpdatedTime = getPost.UpdatedTime,
                    CreatedTime = getPost.CreatedTime,
                    PublishedTime = getPost.PublishedTime,            
                    MetaTitle = getPost.MetaTitle,
                    LangId = getPost.LangId,
                    PublishTypeId = getPost.PublishTypeId,
                    PostContent = getPost.PostContent,
                    Image = getPost.Image,
                    Sorted = getPost.Sorted,
                };
            }
            else
            {
                return null;
            }
        }
        public async Task<int> editPost(PostDto model)
        {
            try
            {
                Post getPost = await _unitOfWork.GetRepository<Post>().FindAsync(x => x.Id == model.Id);

                if (model.Image == null)
                {
                    model.Image = getPost.Image;
                }           

                Post PostGet = await _unitOfWork.GetRepository<Post>().UpdateAsync(new Post
                {
                    Id = model.Id,
                    Image = model.Image,
                    Title = model.Title,
                    VideoSlug = model.VideoSlug,
                    Spot = model.Spot,
                    PostContent = model.PostContent,
                    IsCommentActive = model.IsCommentActive,
                    MetaDescription = model.MetaDescription,
                    Sorted = getPost.Sorted,
                    MetaTitle = model.MetaTitle,
                    IsActive = getPost.IsActive,
                    UpdatedTime = DateTime.Now,
                    CreatedTime = getPost.CreatedTime,
                    PublishTypeId = model.PublishTypeId,
                    publishtype = model.publishtype,
                    PublishedTime = DateTime.Now,
                });

                return PostGet.Id;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> editSortedPost(int itemId, int count)
        {
            try
            {
                var model = _unitOfWork.GetRepository<Post>().FindAsync(x => x.Id == itemId).Result;
                model.Sorted = count;


                if (model.Image == null)
                {
                    model.Image = model.Image;
                }

                Post PostGet = await _unitOfWork.GetRepository<Post>().UpdateAsync(new Post
                {
                    Id = model.Id,
                    Image = model.Image,
                    Title = model.Title,
                    VideoSlug = model.VideoSlug,
                    Spot = model.Spot,
                    PostContent = model.PostContent,
                    IsCommentActive = model.IsCommentActive,
                    MetaDescription = model.MetaDescription,
                    Sorted = model.Sorted,
                    MetaTitle = model.MetaTitle,
                    IsActive = model.IsActive,
                    UpdatedTime = DateTime.Now,
                    CreatedTime = model.CreatedTime,
                    ParentPostId = model.ParentPostId,
                    PublishTypeId = model.PublishTypeId,
                    publishtype = model.publishtype,
                    PublishedTime = DateTime.Now,
                });

                return PostGet.Id;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public bool PostDelete(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<Post>().DeleteAsync(new Post { Id = id });
            return Convert.ToBoolean(result.Result);
        }     
        public async Task<bool> IsActiveEnabled(int id)
        {
            Post getPost = _unitOfWork.GetRepository<Post>().FindAsync(x => x.Id == id).Result;
            if (getPost.IsActive == false)
            {
                getPost.IsActive = true;
                Post model = await _unitOfWork.GetRepository<Post>().UpdateAsync(getPost);
                return getPost != null;
            }
            else
            {
                getPost.IsActive = false;
                Post model = await _unitOfWork.GetRepository<Post>().UpdateAsync(getPost);
                return getPost != null;
            }
        }      
        public List<PostListItemDto> searchDataInPost(string searhNamePost)
        {
            try
            {
                IEnumerable<Post> getPost = _unitOfWork.GetRepository<Post>().Where(null, x => x.OrderByDescending(y => y.Id), "publishtype", null, null);

                if (!String.IsNullOrEmpty(searhNamePost))
                {
                    getPost = getPost.Where(x => x.Title!.Contains(searhNamePost));
                }

                return getPost.Select(x => new PostListItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Spot = x.Spot,
                    VideoSlug = x.VideoSlug,
                    MetaTitle = x.MetaTitle,
                    Image = x.Image,
                    LangId = x.LangId,
                    MetaDescription = x.MetaDescription,
                    PostContent = x.PostContent,          
                    IsActive = x.IsActive,
                    UpdatedTime = x.UpdatedTime,
                    CreatedTime = x.CreatedTime,
                    PublishTypeId = x.PublishTypeId,
                    PublishedTime = x.PublishedTime,
                    IsCommentActive = x.IsCommentActive,
                    Sorted = x.Sorted,
                    publishtype = x.publishtype,

                }).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }    
        public List<TagListItemDto> tagListWithSearch(string searchName)
        {
            IEnumerable<Tags> getTags = _unitOfWork.GetRepository<Tags>().Where(null, x => x.OrderByDescending(y => y.Id), "", null, null);

            if (!String.IsNullOrEmpty(searchName))
            {
                getTags = getTags.Where(x => x.TagName!.Contains(searchName));
            }

            return getTags.Select(x => new TagListItemDto
            {
                Id = x.Id,
                TagName = x.TagName,

            }).ToList();
        }   
        public List<PostListItemDto> PostListWithLastOneToFive()
        {
            IEnumerable<Post> PostList = _unitOfWork.GetRepository<Post>().Where(null, x => x.OrderByDescending(y => y.Id), "publishtype", 1, 5);

            if (PostList != null)
            {
                return PostList.Select(x => new PostListItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Spot = x.Spot,
                    VideoSlug = x.VideoSlug,
                    Image = x.Image,
                    PostContent = x.PostContent,
                    MetaDescription = x.MetaDescription,
                    IsActive = x.IsActive,
                    LangId = x.LangId,
                    UpdatedTime = x.UpdatedTime,
                    CreatedTime = x.CreatedTime,
                    PublishTypeId = x.PublishTypeId,
                    PublishedTime = x.PublishedTime,
                    IsCommentActive = x.IsCommentActive,
                    Sorted = x.Sorted,
                    publishtype = x.publishtype,

                }).ToList();
            }
            else
            {
                return null;
            }
        }       
        public List<PostListItemDto> PostListLoadByScroll(int pageIndex, int pageSize)
        {
            IEnumerable<Post> PostList = _unitOfWork.GetRepository<Post>().Where(null, x => x.OrderBy(y => y.Id), "publishtype", pageIndex, pageSize);

            if (PostList != null)
            {
                return PostList.Select(x => new PostListItemDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Spot = x.Spot,
                    Image = x.Image,
                    MetaDescription = x.MetaDescription,
                    PostContent = x.PostContent,
                    VideoSlug = x.VideoSlug,
                    IsActive = x.IsActive,
                    UpdatedTime = x.UpdatedTime,
                    LangId = x.LangId,
                    CreatedTime = x.CreatedTime,
                    PublishTypeId = x.PublishTypeId,
                    PublishedTime = x.PublishedTime,
                    IsCommentActive = x.IsCommentActive,
                    Sorted = x.Sorted,
                    publishtype = x.publishtype,

                }).ToList();
            }
            else
            {
                return null;
            }
        }
        public List<PostListItemDto> PostListWithWeb()
        {
            IEnumerable<Post> PostList = _unitOfWork.GetRepository<Post>().Where(x => x.IsActive == true && x.LangId == 2, x => x.OrderBy(y => y.PublishedTime), null, null, null);

            if (PostList != null)
            {
                return PostList.Select(x => new PostListItemDto
                {

                    Id = x.Id,
                    Title = x.Title,
                    Spot = x.Spot,
                    Image = x.Image,
                    VideoSlug = x.VideoSlug,
                    PostContent = x.PostContent,
                    IsActive = x.IsActive,
                    LangId = x.LangId,
                    MetaDescription = x.MetaDescription,
                    UpdatedTime = x.UpdatedTime,
                    CreatedTime = x.CreatedTime,
                    PublishTypeId = x.PublishTypeId,
                    PublishedTime = x.PublishedTime,
                    IsCommentActive = x.IsCommentActive,
                    Sorted = x.Sorted,
                    publishtype = x.publishtype,

                }).ToList();
            }
            else
            {
                return null;
            }
        }
        public List<PostListItemDto> PostListWithWebEng()
        {
            IEnumerable<Post> PostList = _unitOfWork.GetRepository<Post>().Where(x => x.IsActive == true && x.LangId == 1, x => x.OrderBy(y => y.PublishedTime), null, null, null);

            if (PostList != null)
            {
                return PostList.Select(x => new PostListItemDto
                {

                    Id = x.Id,
                    Title = x.Title,
                    Spot = x.Spot,
                    Image = x.Image,
                    VideoSlug = x.VideoSlug,
                    PostContent = x.PostContent,
                    IsActive = x.IsActive,
                    LangId = x.LangId,
                    MetaDescription = x.MetaDescription,
                    UpdatedTime = x.UpdatedTime,
                    CreatedTime = x.CreatedTime,
                    PublishTypeId = x.PublishTypeId,
                    PublishedTime = x.PublishedTime,
                    IsCommentActive = x.IsCommentActive,
                    Sorted = x.Sorted,
                    publishtype = x.publishtype,

                }).ToList();
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> ChangeSorted(int id, int sira)
        {
            try
            {
                Post getPost = _unitOfWork.GetRepository<Post>().FindAsync(x => x.Id == id).Result;
                getPost.Sorted = sira;

                Post model = await _unitOfWork.GetRepository<Post>().UpdateAsync(getPost);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }    
      
        public List<PostListItemDto> PostListJsonData()
        {
            IEnumerable<Post> PostList = _unitOfWork.GetRepository<Post>().Where(x => x.IsActive == true, x => x.OrderBy(y => y.Id), "", null, null);

            if (PostList != null)
            {
                return PostList.Select(x => new PostListItemDto
                {

                    Id = x.Id,
                    Title = x.Title,
                    Spot = x.Spot,
                    Image = x.Image,
                    VideoSlug = x.VideoSlug,
                    PostContent = x.PostContent,
                    IsActive = x.IsActive,
                    UpdatedTime = x.UpdatedTime,
                    LangId = x.LangId,
                    CreatedTime = x.CreatedTime,
                    MetaDescription = x.MetaDescription,
                    PublishTypeId = x.PublishTypeId,
                    PublishedTime = x.PublishedTime,
                    IsCommentActive = x.IsCommentActive,
                    Sorted = x.Sorted,

                }).ToList();
            }
            else
            {
                return null;
            }
        }
        public List<PostListItemDto> PostListWithWebTr()
        {
            IEnumerable<Post> PostList = _unitOfWork.GetRepository<Post>().Where(x => x.IsActive == true && x.LangId == 2, x => x.OrderBy(y => y.PublishedTime), null, null, null);

            if (PostList != null)
            {
                return PostList.Select(x => new PostListItemDto
                {

                    Id = x.Id,
                    Title = x.Title,
                    Spot = x.Spot,
                    Image = x.Image,
                    VideoSlug = x.VideoSlug,
                    PostContent = x.PostContent,
                    IsActive = x.IsActive,
                    LangId = x.LangId,
                    MetaDescription = x.MetaDescription,
                    UpdatedTime = x.UpdatedTime,
                    CreatedTime = x.CreatedTime,
                    PublishTypeId = x.PublishTypeId,
                    PublishedTime = x.PublishedTime,
                    IsCommentActive = x.IsCommentActive,
                    Sorted = x.Sorted,
                    publishtype = x.publishtype,

                }).ToList();
            }
            else
            {
                return null;
            }
        }
        public async Task<int> insertLanguageSwitch(PostLanguageDto model)
        {
            try
            {
                PostLanguage createPost = await _unitOfWork.GetRepository<PostLanguage>().AddAsync(new PostLanguage
                {
                   postEngTitle = model.PostEngTitle,
                   postTrTitle = model.PostTrTitle
                });

                return createPost.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public PostLanguageDto getPostLanguage(string title)
        {
            //Post getPost = _unitOfWork.GetRepository<Post>().FindAsync(x => x.Id == id).Result;

            PostLanguage getPost = _unitOfWork.GetRepository<PostLanguage>().Where(x => x.postTrTitle == title || x.postEngTitle == title).SingleOrDefault();

            if (getPost != null)
            {
                return new PostLanguageDto
                {
                    PostEngTitle = getPost.postEngTitle,
                    PostTrTitle = getPost.postTrTitle
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<int> editPostLanguageTr(PostLanguageDto model)
        {
            try
            {
                PostLanguage getPost = await _unitOfWork.GetRepository<PostLanguage>().FindAsync(x => x.Id == model.Id);

                PostLanguage PostGet = await _unitOfWork.GetRepository<PostLanguage>().UpdateAsync(new PostLanguage
                {
                    Id = model.Id,
                    postTrTitle = model.PostTrTitle,
                    postEngTitle = getPost.postEngTitle,
                });

                return PostGet.Id;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> editPostLanguageEn(PostLanguageDto model)
        {
            try
            {
                PostLanguage getPost = await _unitOfWork.GetRepository<PostLanguage>().FindAsync(x => x.Id == model.Id);

                PostLanguage PostGet = await _unitOfWork.GetRepository<PostLanguage>().UpdateAsync(new PostLanguage
                {
                    Id = model.Id,
                    postTrTitle = getPost.postTrTitle,
                    postEngTitle = model.PostEngTitle,
                });

                return PostGet.Id;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public PostDto getPostToTitle(string Title)
        {
            Post getPost = _unitOfWork.GetRepository<Post>().Where(x => x.Title == Title, x => x.OrderBy(x => x.UpdatedTime), "publishtype", null, null).SingleOrDefault();

            if (getPost != null)
            {
                return new PostDto
                {
                    Id = getPost.Id,
                    Title = getPost.Title,
                    Spot = getPost.Spot,
                    IsActive = getPost.IsActive,
                    VideoSlug = getPost.VideoSlug,
                    MetaDescription = getPost.MetaDescription,
                    IsCommentActive = getPost.IsCommentActive,
                    UpdatedTime = getPost.UpdatedTime,
                    CreatedTime = getPost.CreatedTime,
                    PublishedTime = getPost.PublishedTime,
                    MetaTitle = getPost.MetaTitle,
                    LangId = getPost.LangId,
                    PublishTypeId = getPost.PublishTypeId,
                    PostContent = getPost.PostContent,
                    Image = getPost.Image,
                    Sorted = getPost.Sorted,
                };
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
