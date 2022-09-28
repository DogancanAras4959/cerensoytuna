using AutoMapper;
using cerensoytuna.COMMON.DTOS.LangDTO;
using cerensoytuna.COMMON.DTOS.PostDTO.PostLanguageDTO;
using cerensoytuna.COMMON.Helpers;
using cerensoytuna.COMMON.PostDTO;
using cerensoytuna.COMMON.PostDTO.MediaDTO;
using cerensoytuna.COMMON.PostDTO.PublishTypeDTO;
using cerensoytuna.COMMON.PostDTO.TagDTO;
using cerensoytuna.COMMON.PostDTO.TagPostDTO;
using cerensoytuna.COMMON.SeoDTO;
using cerensoytuna.COMMON.SeoDTO.SeoMetaDto;
using cerensoytuna.editor.Models.LangModel;
using cerensoytuna.editor.Models.MediaModel;
using cerensoytuna.editor.Models.PostModel;
using cerensoytuna.editor.Models.PostModel.PostLanguage;
using cerensoytuna.editor.Models.PostModel.PublishTypeModel;
using cerensoytuna.editor.Models.PostModel.TagModel;
using cerensoytuna.editor.Models.PostModel.TagPostModel;
using cerensoytuna.editor.Models.SeoModel;
using cerensoytuna.editor.Models.SeoModel.SeoMetaModel;
using cerensoytuna.ENGINE.Interface;
using cerensoytuna.ENGINES.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace cerensoytuna.editor.Controllers
{
    public class gonderiController : Controller
    {

        #region Fields / Constructure

        private readonly IMapper _mapper;
        private readonly IPostService _newService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _userService;
        private readonly ISettingService _settingService;
        private readonly ISeoService _seoService;
        private readonly ILangService _langService;
        public gonderiController(IMapper mapper, IPostService newsService, IWebHostEnvironment webHostEnvironment, IUserService userService, ISettingService settingService, ISeoService seoService, ILangService langService)
        {
            _mapper = mapper;
            _newService = newsService;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
            _settingService = settingService;
            _seoService = seoService;
            _langService = langService;
        }

        #endregion

        #region Gönderiler

        [HttpGet]
        [Authorize]
        public IActionResult gonderiler(int? pageNumber, string searchstring, int? CategoryId, int? UserId)
        {
            try
            {
                int pageSize = 50;
                List<PostListItemModel> haberlist = null;

                var newListToLanguageTR = _mapper.Map<List<PostListItemDto>, List<PostListItemModel>>(_newService.PostListWithWebTr());
                ViewBag.SelectLanguage = new SelectList(newListToLanguageTR, "Id", "Title");

                var newListToLanguageEng = _mapper.Map<List<PostListItemDto>, List<PostListItemModel>>(_newService.PostListWithWebEng());
                ViewBag.SelectLanguageEng = new SelectList(newListToLanguageEng, "Id", "Title");

                if (searchstring != "" && searchstring != null)
                {
                    haberlist = _mapper.Map<List<PostListItemDto>, List<PostListItemModel>>(_newService.searchDataInPost(searchstring));
                    return View(PaginationList<PostListItemModel>.Create(haberlist.ToList(), pageNumber ?? 1, pageSize));
                }

                haberlist = _mapper.Map<List<PostListItemDto>, List<PostListItemModel>>(_newService.PostList());
                return View(PaginationList<PostListItemModel>.Create(haberlist.ToList(), pageNumber ?? 1, pageSize));
            }
            catch (Exception ex)
            {
                TempData["HataMesaji"] = ex.ToString();
                return RedirectToAction("hata", "yonetim");
            }

        }

        [HttpGet]
        [Authorize]
        public IActionResult gonderiolustur()
        {
            try
            {
                LoadData();
                return View(new PostCreateViewModel());
            }
            catch (Exception ex)
            {
                TempData["HataMesaji"] = ex.ToString();
                return RedirectToAction("hata", "yonetim");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> gonderiekle(PostCreateViewModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!await _newService.PostIfExists(model.Title))
                    {
                        if (file != null && file.Length > 0)
                        {
                            if (model.PublishTypeId == 999)
                            {
                                ViewBag.Hata = "Gönderi tipi seçimiyle ilgili bir sorun çıktı. Haber tipi seçiniz"!;
                                LoadData();
                                return View(model);
                            }
                            else
                            {

                                model.Image = SaveImageProcess.ImageInsert(file, "Admin");
                                int resultId = Convert.ToInt32(await _newService.createPost(_mapper.Map<PostCreateViewModel, PostDto>(model)));

                                if (resultId > 0)
                                {

                                    if (!string.IsNullOrEmpty(model.Tag))
                                    {
                                        if (model.Tag[^1] == ',')
                                        {
                                            await _newService.InsertTagToProduct(model.Tag[0..^1], resultId);
                                        }
                                        else
                                        {
                                            await _newService.InsertTagToProduct(model.Tag, resultId);
                                        }
                                    }

                                    return RedirectToAction("gonderiler", "gonderi");
                                }
                                else
                                {

                                    ViewBag.Hata = "Gönderiniz oluşturulurken bir hata meydana geldi! Etiketlerinizi kontrol edin"!;
                                    LoadData();
                                    return View(model);
                                }
                            }
                        }
                        else
                        {
                            ViewBag.Hata = "Gönderi için öne çıkan görsel girilmelidir. Haber bu yüzden oluşturulamadı"!;
                            LoadData();
                            return View(model);
                        }
                    }
                    else
                    {
                        ViewBag.Hata = "Oluşturmak istediğiniz gönderi zaten sistemde bulunuyor"!;
                        LoadData();
                        return View(model);
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["HataMesajı"] = ex.ToString();
                return RedirectToAction("hata", "yonetim");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> gonderiduzenle(int id, string durum = "")
        {
            try
            {

                #region GetTags

                var tags = _mapper.Map<List<TagPostListItemDto>, List<TagPostListViewModel>>(_newService.tagsListWithPostByPostId(id));
                var news = _mapper.Map<PostDto, PostEditViewModel>(_newService.getPost(id));

                List<string> list = new List<string>();

                foreach (var item in tags)
                {
                    list.Add(item.tag.TagName);
                }

                string[] tagsList = list.ToArray();

                for (int i = 0; i < tagsList.Count(); i++)
                {
                    if (news.Tag != null)
                    {
                        news.Tag = news.Tag + "," + tagsList[i];
                    }
                    else
                    {
                        news.Tag = tagsList[i];
                    }
                }

                #endregion

                #region Load Data

                var publishTypeList = _mapper.Map<List<PublishTypeListItem>, List<PublishTypeListViewModel>>(_newService.publishTypeList());
                ViewBag.PublishTypes = new SelectList(publishTypeList, "Id", "TypeName", news.PublishTypeId);

                var langList = _mapper.Map<List<LangListItemDto>, List<LangListViewModel>>(_langService.langList());
                ViewBag.Langs = new SelectList(langList, "Id", "langTitle");

                var newList = _mapper.Map<List<PostListItemDto>, List<PostListItemModel>>(_newService.PostList());
                ViewBag.News = newList;

                if (durum == "")
                {
                    TempData["durum"] = null;
                }
                else
                {
                    TempData["durum"] = durum;
                }

                #endregion

                await seoCreateOrUpdate(news);

                return View(news);
            }
            catch (Exception ex)
            {
                TempData["HataMesaji"] = ex.ToString();
                return RedirectToAction("hata", "yonetim");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> gonderiguncelle(PostEditViewModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var news = _mapper.Map<PostDto, PostEditViewModel>(_newService.getPost(model.Id));

                    if (model.PublishTypeId == 999)
                    {
                        ViewBag.Hata = "Haber tipi seçimiyle ilgili bir sorun çıktı. Haber tipi seçiniz"!;
                        LoadData();
                        return View(news);
                    }
                    else
                    {

                        if (file != null)
                        {

                            model.Image = SaveImageProcess.ImageInsert(file, "Admin");

                            int resultId = Convert.ToInt32(await _newService.editPost(_mapper.Map<PostEditViewModel, PostDto>(model)));

                            if (resultId > 0)
                            {
                                if (!string.IsNullOrEmpty(model.Tag))
                                {
                                    if (model.Tag[^1] == ',')
                                    {
                                        await _newService.InsertTagToProduct(model.Tag[0..^1], resultId);
                                    }
                                    else
                                    {
                                        await _newService.InsertTagToProduct(model.Tag, resultId);
                                    }
                                }
                                return RedirectToAction("gonderiduzenle", "gonderi");
                            }
                        }
                        else
                        {

                            int resultId = Convert.ToInt32(await _newService.editPost(_mapper.Map<PostEditViewModel, PostDto>(model)));

                            if (resultId > 0)
                            {
                                if (!string.IsNullOrEmpty(model.Tag))
                                {
                                    if (model.Tag[^1] == ',')
                                    {
                                        await _newService.InsertTagToProduct(model.Tag[0..^1], resultId);
                                    }
                                    else
                                    {
                                        await _newService.InsertTagToProduct(model.Tag, resultId);
                                    }
                                }
                                return RedirectToAction("gonderiduzenle", "gonderi", new { Id = model.Id });
                            }
                        }
                    }

                    LoadData();
                    return RedirectToAction("gonderiduzenle", "gonderi", new { Id = model.Id });
                }
                else
                {
                    LoadData();
                    return RedirectToAction("gonderiduzenle", "gonderi", new { Id = model.Id });
                }
            }
            catch (Exception ex)
            {
                TempData["HataMesaji"] = ex.ToString();
                return RedirectToAction("hata", "yonetim");
            }
        }

        [Authorize]
        public IActionResult habersil(int id)
        {
            try
            {
                if (_newService.PostDelete(id))
                {
                    return RedirectToAction("haberler", "haber");
                }
                else
                {
                    return RedirectToAction("haberler", "haber");
                }
            }
            catch (Exception ex)
            {
                TempData["HataMesaji"] = ex.ToString();
                return RedirectToAction("hata", "yonetim");
            }
        }

        public async Task<IActionResult> gonderiyiaktifet(int id)
        {
            try
            {
                if (await _newService.IsActiveEnabled(id))
                {
                    return RedirectToAction(nameof(gonderiler));
                }
                return View(nameof(gonderiler));
            }
            catch (Exception ex)
            {
                TempData["HataMesaji"] = ex.ToString();
                return RedirectToAction("hata", "yonetim");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult etiketler(int? pageNumber, string tagNameSearch)
        {
            try
            {
                var etiketlerHaberler = _mapper.Map<List<TagPostListItemDto>, List<TagPostListViewModel>>(_newService.tagsListWithPost());
                ViewBag.EtiketHaberler = etiketlerHaberler;

                int pageSize = 20;
                List<TagListViewModel> tags = null;

                if (tagNameSearch != null && tagNameSearch != "")
                {
                    tags = _mapper.Map<List<TagListItemDto>, List<TagListViewModel>>(_newService.tagListWithSearch(tagNameSearch));
                    return View(PaginationList<TagListViewModel>.Create(tags.ToList(), pageNumber ?? 1, pageSize));
                }

                tags = _mapper.Map<List<TagListItemDto>, List<TagListViewModel>>(_newService.tagList());
                return View(PaginationList<TagListViewModel>.Create(tags.ToList(), pageNumber ?? 1, pageSize));
            }
            catch (Exception ex)
            {
                TempData["HataMesaji"] = ex.ToString();
                return RedirectToAction("hata", "yonetim");
            }
        }

        [Authorize]
        public IActionResult etiketegorehaberler(int Id, int? pageNumber)
        {
            try
            {
                int pageSize = 20;
                var etiket = _mapper.Map<TagDto, TagEditViewModel>(_newService.tagGet(Id));
                ViewBag.Tag = etiket;

                var etiketlerHaberler = _mapper.Map<List<TagPostListItemDto>, List<TagPostListViewModel>>(_newService.tagsListWithPostById(Id));
                return View(PaginationList<TagPostListViewModel>.Create(etiketlerHaberler.ToList(), pageNumber ?? 1, pageSize));
            }
            catch (Exception ex)
            {
                TempData["HataMesaji"] = ex.ToString();
                return RedirectToAction("hata", "yonetim");
            }
        }

        [Authorize]
        public IActionResult etiketsil(int id)
        {
            try
            {
                if (_newService.tagDelete(id))
                {
                    return RedirectToAction(nameof(etiketler));
                }
                else
                {
                    return RedirectToAction(nameof(etiketler));
                }
            }
            catch (Exception ex)
            {
                TempData["HataMesaji"] = ex.ToString();
                return RedirectToAction("hata", "yonetim");
            }
        }

        [HttpPost]
        public IActionResult uploadimages(IList<IFormFile> files)
        {
            var filePath = "";
            foreach (IFormFile photo in Request.Form.Files)
            {
                filePath = SaveImageProcess.ImageInsert(photo, "Admin");
            }
            return Json(new { url = "https://uploads.gazetekapi.com/images/" + filePath });
        }

        [HttpPost]
        public async Task<IActionResult> tedaviyiesle(int postIds, int selectIds)
        {
            var news = _mapper.Map<PostDto, PostEditViewModel>(_newService.getPost(postIds));
            var selectedNews = _mapper.Map<PostDto, PostEditViewModel>(_newService.getPost(selectIds));
            var getLanguageSyncNews = _mapper.Map<PostLanguageDto, EditPostLanguageViewModel>(_newService.getPostLanguage(news.Title));

            if (getLanguageSyncNews != null)
            {
                
                if (news.LangId == 1)
                {
                    getLanguageSyncNews.PostTrTitle = selectedNews.Title;
                    int resultId = Convert.ToInt32(await _newService.editPostLanguageTr(_mapper.Map<EditPostLanguageViewModel, PostLanguageDto>(getLanguageSyncNews)));
                }
                else if (news.LangId == 2)
                {
                    getLanguageSyncNews.PostEngTitle = selectedNews.Title;
                    int resultId = Convert.ToInt32(await _newService.editPostLanguageEn(_mapper.Map<EditPostLanguageViewModel, PostLanguageDto>(getLanguageSyncNews)));
                }
                return RedirectToAction("gonderiler", "gonderi");

            }
            else
            {
                CreatePostLanguageViewModel modelPost = new CreatePostLanguageViewModel();

                if (news.LangId == 1)
                {
                    modelPost.PostEngTitle = news.Title;
                    modelPost.PostTrTitle = selectedNews.Title;
                }            

                else if (news.LangId == 2)
                {
                    modelPost.PostTrTitle = news.Title;
                    modelPost.PostEngTitle = selectedNews.Title;
                }

                int resultLanguageId = Convert.ToInt32(await _newService.insertLanguageSwitch(_mapper.Map<CreatePostLanguageViewModel, PostLanguageDto>(modelPost)));

                return RedirectToAction("gonderiler", "gonderi");
            }
        }
        #endregion

        #region OrtamMedyası

        public IActionResult ortammedyasi(int? pagenumber)
        {
            int pageSize = 50;
            List<MediaListViewModel> mediaList = null;

            mediaList = _mapper.Map<List<MediaListItemDto>, List<MediaListViewModel>>(_newService.mediaList());
            return View(PaginationList<MediaListViewModel>.Create(mediaList.ToList(), pagenumber ?? 1, pageSize));

        }

        public async Task<IActionResult> medyaekle(IFormFile fileupload)
        {
            if (fileupload != null)
            {

                MediaCreateViewModel model = new MediaCreateViewModel
                {
                    Slug = SaveImageProcess.VideoInsert(fileupload, "Videos"),
                    Title = fileupload.FileName,
                };

                int resultId = Convert.ToInt32(await _newService.insertMedia(_mapper.Map<MediaCreateViewModel, MediaDto>(model)));

            }
            return View();
        }

        #endregion

        #region Extend Methods

        public void LoadData()
        {
            var publishTypeList = _mapper.Map<List<PublishTypeListItem>, List<PublishTypeListViewModel>>(_newService.publishTypeList());
            ViewBag.PublishTypes = new SelectList(publishTypeList, "Id", "TypeName");

            var langList = _mapper.Map<List<LangListItemDto>, List<LangListViewModel>>(_langService.langList());
            ViewBag.Langs = new SelectList(langList, "Id", "langTitle");

        }

        #endregion

        #region Seo Methods

        private async Task seoCreateOrUpdate(PostEditViewModel news)
        {
            #region SEO Create or Updates

            var getSeoIfExists = _mapper.Map<SeoScoreDto, SeoScoreViewModel>(_seoService.GetSeoScoreByPostId(news.Id));

            if (getSeoIfExists == null)
            {

                SeoScoreCreateViewModel newModel = new SeoScoreCreateViewModel();
                string code = RandomStringForUniqueCode(20);
                newModel.PostId = news.Id;

                int resultId = Convert.ToInt32(await _seoService.CreateSeoScore(_mapper.Map<SeoScoreCreateViewModel, SeoScoreDto>(newModel), code));

                if (resultId > 0 && resultId != -1)
                {
                    var getSeo = _mapper.Map<SeoScoreDto, SeoScoreViewModel>(_seoService.GetSeoScoreByPostId(news.Id));

                    #region Seo Meta Create

                    await _seoService.CreateSeoMetaToSeoScore(getSeo.Id);
                    //_seoService.UpdateSeoScoreAfterCreateTask(getSeo.Id);
                    // Bu mesele çok önemli. IsCreated false'a çevrilmeli
                    #endregion

                    LevelAnalyze(getSeo.Id);
                    ViewBag.SeoScore = getSeo;

                    #region Seo Meta Create

                    var listSeoMetas = _mapper.Map<List<SeoMetaListItemDto>, List<SeoMetaListViewModel>>(_seoService.listSeoMetasBySeoScoreId(getSeo.Id));

                    if (listSeoMetas.Count == 0 && listSeoMetas.Where(x => x.IsDone == true).ToList().Count == 0)
                    {
                        if (getSeoIfExists.IsFinished == false && getSeoIfExists.IsCreated == true)
                        {
                            await _seoService.CreateSeoMetaToSeoScore(getSeoIfExists.Id);
                            //_seoService.UpdateSeoScoreAfterCreateTask(getSeoIfExists.Id);
                            // Bu mesele çok önemli. IsCreated false'a çevrilmeli
                        }
                    }
                    else
                    {
                        ViewBag.listSeoMeta = listSeoMetas;
                    }

                    #endregion

                }
            }

            else
            {

                #region Seo Meta Create

                var listSeoMetas = _mapper.Map<List<SeoMetaListItemDto>, List<SeoMetaListViewModel>>(_seoService.listSeoMetasBySeoScoreId(getSeoIfExists.Id));

                if (listSeoMetas.Count == 0 && listSeoMetas.Where(x => x.IsDone == true).ToList().Count == 0)
                {
                    if (getSeoIfExists.IsFinished == false && getSeoIfExists.IsCreated == true)
                    {
                        await _seoService.CreateSeoMetaToSeoScore(getSeoIfExists.Id);
                        //_seoService.UpdateSeoScoreAfterCreateTask(getSeoIfExists.Id);
                        // Bu mesele çok önemli. IsCreated false'a çevrilmeli
                    }
                }
                else
                {
                    ViewBag.listSeoMeta = listSeoMetas;
                }

                #endregion

                ViewBag.SeoScore = getSeoIfExists;
                LevelAnalyze(getSeoIfExists.Id);
            }

            #endregion
        }

        public IActionResult RefreshSeoScore(int Id)
        {
            var news = _mapper.Map<PostDto, PostEditViewModel>(_newService.getPost(Id));
            AnalyzePostToSeoAsync(news);
            return RedirectToAction("HaberDuzenle", "Haber", new { Id = news.Id, durum = "" });
        }

        private static Random random = new Random();
        public static string RandomStringForUniqueCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void AnalyzePostToSeoAsync(PostEditViewModel model)
        {

            #region fields

            var seoScoreByNewsId = _mapper.Map<SeoScoreDto, SeoScoreViewModel>(_seoService.GetSeoScoreByPostId(model.Id));

            var listSeoMetas = _mapper.Map<List<SeoMetaListItemDto>, List<SeoMetaListViewModel>>(_seoService.listSeoMetasBySeoScoreIdByAnalyze(seoScoreByNewsId.Id));

            List<SeoMetaListViewModel> newList = new List<SeoMetaListViewModel>();

            var tags = _mapper.Map<List<TagPostListItemDto>, List<TagPostListViewModel>>(_newService.tagsListWithPostByPostId(model.Id));

            #endregion

            int count = 0;
            int point = 0;

            foreach (var item in listSeoMetas)
            {

                count += 1;

                if (item.metaCode.Contains("b-3") && item.IsDone == false)
                {
                    if (model.MetaTitle != null || model.MetaTitle == "")
                        newList.Add(item);
                }

                if (item.metaCode.Contains("b-1") && item.IsDone == false)
                {
                    if (model.MetaTitle != null || model.MetaTitle == "")
                    {
                        int lengthOfTitle = model.Title.Length;
                        bool isRight = (lengthOfTitle < 35) ? false :
                            (lengthOfTitle > 65) ? false :
                            (lengthOfTitle > 35 && lengthOfTitle < 65) ? true :
                            (lengthOfTitle == 0) ? false : false;

                        if (isRight)
                            newList.Add(item);
                    }

                }

                if (item.metaCode.Contains("i-2") && item.IsDone == false)
                {
                    if (model.Image != null || model.Image == "")
                        newList.Add(item);
                }

                if (item.metaCode.Contains("d-3") && item.IsDone == false)
                {
                    if (model.Spot != null || model.Spot == "")
                        newList.Add(item);
                }

                if (item.metaCode.Contains("d-1") && item.IsDone == false)
                {
                    int lengthOfTitle = model.Spot.Length;
                    bool isRight = (lengthOfTitle < 120) ? false :
                        (lengthOfTitle > 160) ? false :
                        (lengthOfTitle > 120 && lengthOfTitle < 160) ? true :
                        (lengthOfTitle == 0) ? false : false;

                    if (isRight)
                        newList.Add(item);
                }

                if (item.metaCode.Contains("i-1") && item.IsDone == false)
                {
                    if (model.Image != null || model.Image == "")
                    {
                        string extension = Path.GetExtension(model.Image);
                        bool isRight = extension != ".gif" ? true : false;

                        if (isRight)
                            newList.Add(item);
                    }
                }

                if (item.metaCode.Contains("k-1") && item.IsDone == false)
                {

                    int countTags = tags.Count();

                    bool isRight = countTags < 5 ? false :
                                   countTags > 8 ? false :
                                   countTags >= 5 && countTags <= 8 ? true : false;

                    if (isRight)
                        newList.Add(item);
                }

                if (item.metaCode.Contains("d-2") && item.IsDone == false)
                {
                    if (model.Spot != null || model.Spot == "")
                    {
                        int tagCount = 0;
                        foreach (var tagItem in tags)
                        {
                            if (model.Spot.Contains(tagItem.tag.TagName))
                            {
                                tagCount++;
                            }
                        }
                        bool isRight = tagCount > 0 ? true :
                                          tagCount == 0 ? false : false;

                        if (isRight)
                            newList.Add(item);
                    }

                }

                if (item.metaCode.Contains("b-2") && item.IsDone == false)
                {
                    if (model.MetaTitle != null || model.MetaTitle == "")
                    {
                        int tagCount = 0;
                        foreach (var tagItem in tags)
                        {
                            if (model.MetaTitle.Contains(tagItem.tag.TagName))
                            {
                                tagCount++;
                            }
                        }
                        bool isRight = tagCount > 0 ? true :
                                         tagCount == 0 ? false : false;

                        if (isRight)
                            newList.Add(item);
                    }
                }

                if (count == 10)
                {
                    point += ChangeSeoMetaStatus(newList);
                    _seoService.IncreaseSeoScore(seoScoreByNewsId.Id, point);
                }
            }

        }

        private int ChangeSeoMetaStatus(List<SeoMetaListViewModel> newList)
        {
            int point = 0;
            foreach (var item in newList)
            {
                _seoService.SeoMetaIsDone(item.Id);
                point += item.Point;
            }

            return point;
        }

        public string ScoreAnalyze(int seoScoreId)
        {
            var getSeoIfExists = _mapper.Map<SeoScoreDto, SeoScoreViewModel>(_seoService.GetSeoScore(seoScoreId));
            if (getSeoIfExists.Amount == 0)
            {
                return "Skor Yok";
            }
            else if (getSeoIfExists.Amount >= 1 && getSeoIfExists.Amount <= 34)
            {
                return "Kötü";
            }
            else if (getSeoIfExists.Amount > 34 && getSeoIfExists.Amount <= 60)
            {
                return "Ortalama";
            }
            else if (getSeoIfExists.Amount > 60 && getSeoIfExists.Amount <= 85)
            {
                return "İyi";
            }
            else if (getSeoIfExists.Amount > 85 && getSeoIfExists.Amount <= 9)
            {
                return "Çok İyi";
            }
            else
            {
                return "Hata";
            }
        }

        private void LevelAnalyze(int Id)
        {

            // 1-34 = Kötü | 34-60 = Ortalama | 60-85 = İyi | 85-99 = Çok İyi //
            var getSeoIfExists = _mapper.Map<SeoScoreDto, SeoScoreViewModel>(_seoService.GetSeoScore(Id));
            int levelCase = getSeoIfExists.Level;

            switch (levelCase)
            {
                case 0:
                    ViewData["scoreNote"] = ScoreAnalyze(getSeoIfExists.Id);
                    break;
                case 1:
                    ViewData["scoreNote"] = ScoreAnalyze(getSeoIfExists.Id);
                    ViewData["progress-color"] = "#ea553d";
                    ViewData["bg"] = "text-danger";
                    break;

                case 2:
                    ViewData["scoreNote"] = ScoreAnalyze(getSeoIfExists.Id);
                    ViewData["progress-color"] = "#fb8c00";
                    ViewData["bg"] = "text-warning";
                    break;

                case 3:
                    ViewData["scoreNote"] = ScoreAnalyze(getSeoIfExists.Id);
                    ViewData["progress-color"] = "#67a8e4";
                    ViewData["bg"] = "text-primary";
                    break;
                case 4:
                    ViewData["scoreNote"] = ScoreAnalyze(getSeoIfExists.Id);
                    ViewData["progress-color"] = "#4ac18e";
                    ViewData["bg"] = "text-success";
                    break;

                default:
                    ViewData["scoreNote"] = "Bilgi Alınamadı!";
                    break;
            }
        }

        #endregion
    }
}
