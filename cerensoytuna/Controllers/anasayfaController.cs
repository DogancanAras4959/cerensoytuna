using AutoMapper;
using cerensoytuna.COMMON.DTOS.PostDTO.PostLanguageDTO;
using cerensoytuna.COMMON.Helpers;
using cerensoytuna.COMMON.PostDTO;
using cerensoytuna.COMMON.PostDTO.TagPostDTO;
using cerensoytuna.CORE.EmailConfig;
using cerensoytuna.ENGINES.Interface;
using cerensoytuna.Models;
using cerensoytuna.Models.EmailModel;
using cerensoytuna.Models.PostLanguageModel;
using cerensoytuna.Models.PostModel;
using cerensoytuna.Models.TagPostModel;
using cerensoytuna.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cerensoytuna.Controllers
{
    public class anasayfaController : Controller
    {
        private readonly LocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly IEmailSender _emailSender;
        public anasayfaController(LocalizationService localizationService, IMapper mapper, IPostService postService, IEmailSender emailSender)
        {
            _localizationService = localizationService;
            _mapper = mapper;
            _emailSender = emailSender;
            _postService = postService;
        }

        public IActionResult sayfa()
        {

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Dr. Ceren Ayözger | Doktor & Dişçi";
            meta.Keywords = "Çocuk, Klinik, Ayözger, Dentist, Bakım";
            meta.Description = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.ogTitle = "Dr. Ceren Ayözger | Doktor & Dişçi";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerensoytuna.com/";
            ViewBag.Meta = meta;

            #endregion

            string name = _localizationService.GetLocalizedHtmlString("ANA SAYFA");
            List<PostListViewModel> haberlist = null;


            List<TagPostListViewModel> tagNewList = _mapper.Map<List<TagPostListItemDto>, List<TagPostListViewModel>>(_postService.tagsListWithPostWeb());
            ViewBag.TagNews = tagNewList;

            if (name == "Home")
            {
                haberlist = _mapper.Map<List<PostListItemDto>, List<PostListViewModel>>(_postService.PostListWithWebEng());
                ViewBag.Gonderiler = haberlist;
                return View();
            }
            else
            {
                haberlist = _mapper.Map<List<PostListItemDto>, List<PostListViewModel>>(_postService.PostListWithWeb());
                ViewBag.Gonderiler = haberlist;
                return View();
            }     
        }

        public IActionResult iletisim()
        {
            string menuName = _localizationService.GetLocalizedHtmlString("İLETİŞİM");

            if (menuName == "Contact Us")
            {
                return RedirectToAction("contact", "home");
            }

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "İletişim";
            meta.Keywords = "Çocuk, Klinik, Ayözger, Dentist, Bakım";
            meta.Description = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.ogTitle = "Dr. Ceren Ayözger | Doktor & Dişçi";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerensoytuna.com/";
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        public IActionResult randevual()
        {
            string menuName = _localizationService.GetLocalizedHtmlString("RANDEVU");

            if (menuName == "Make An Appoinment")
            {
                return RedirectToAction("booknow", "home");
            }

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Randevu Al";
            meta.Keywords = "Çocuk, Klinik, Ayözger, Dentist, Bakım";
            meta.Description = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.ogTitle = "Dr. Ceren Ayözger | Doktor & Dişçi";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerensoytuna.com/";
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        public IActionResult hakkimizda()
        {
            string menuName = _localizationService.GetLocalizedHtmlString("KLİNİĞİMİZ");

            if (menuName == "Our Clinic")
            {
                return RedirectToAction("aboutus", "home");
            }

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Hakkımızda";
            meta.Keywords = "Çocuk, Klinik, Ayözger, Dentist, Bakım";
            meta.Description = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.ogTitle = "Dr. Ceren Ayözger | Doktor & Dişçi";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerensoytuna.com/";
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        public IActionResult islemler(int? pageNumber)
        {
            string menuName = _localizationService.GetLocalizedHtmlString("TEDAVİLER");
            
            if (menuName == "Treatments")
            {
                return RedirectToAction("treatments","home");
            }

            ViewBag.TitlePage = "Tedaviler";
            ViewBag.SubTitle = "Ana Sayfa";

            ViewBag.LangEng = "İngilizce";
            ViewBag.LangTr = "Türkçe";

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "İşlemlerimiz";
            meta.Keywords = "Çocuk, Klinik, Ayözger, Dentist, Bakım";
            meta.Description = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.ogTitle = "Dr. Ceren Ayözger | Doktor & Dişçi";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerensoytuna.com/";
            ViewBag.Meta = meta;

            #endregion

            int pageSize = 20;

            var haberlist = _mapper.Map<List<PostListItemDto>, List<PostListViewModel>>(_postService.PostListWithWeb());
            ViewBag.Gonderiler = PaginationList<PostListViewModel>.Create(haberlist.ToList(), pageNumber ?? 1, pageSize);

            return View();
        }

        public IActionResult doktorumuz()
        {
            string menuName = _localizationService.GetLocalizedHtmlString("DR. CEREN SOYTUNA");

            if (menuName == "Dt. Ceren Ayözger")
            {
                return RedirectToAction("ourdoctor", "home");
            }

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Doktorumuz | Ceren Ayözger";
            meta.Keywords = "Çocuk, Klinik, Ayözger, Dentist, Bakım";
            meta.Description = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.ogTitle = "Dr. Ceren Ayözger | Doktor & Dişçi";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerensoytuna.com/";
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        public IActionResult gizlilik()
        {

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Gizlilik Politikası";
            meta.Keywords = "Çocuk, Klinik, Ayözger, Dentist, Bakım";
            meta.Description = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.ogTitle = "Dr. Ceren Ayözger | Doktor & Dişçi";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerensoytuna.com/";
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        public IActionResult cerez()
        {

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Çerez Politikası";
            meta.Keywords = "Çocuk, Klinik, Ayözger, Dentist, Bakım";
            meta.Description = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.ogTitle = "Dr. Ceren Ayözger | Doktor & Dişçi";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerensoytuna.com/";
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        [HttpGet("islem/{Id}/{Title}", Name = "islem")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)] 
        public IActionResult islem(int Id, string Title)
        {

            var newsGet = _mapper.Map<PostDto, PostEditViewModel>(_postService.getPost(Id));
            string friendlyTitle = Title;

            if (_localizationService.GetLocalizedHtmlString("Post") == "Post")
            {
                var postLanguage = _mapper.Map<PostLanguageDto, EditPostLanguageViewModel>(_postService.getPostLanguage(newsGet.Title));
                var newsGetToLanguage = _mapper.Map<PostDto, PostEditViewModel>(_postService.getPostToTitle(postLanguage.postEngTitle));
                return RedirectToAction("treatment", "home", new { Id = newsGetToLanguage.Id, Title = HtmlToPlainText(newsGetToLanguage.Title) });
            }

            List<TagPostListViewModel> tags = _mapper.Map<List<TagPostListItemDto>, List<TagPostListViewModel>>(_postService.tagsListWithPostByPostId(newsGet.Id));

            ViewBag.TagList = tags;

            #region Meta

            MetaViewModel meta = new MetaViewModel();
            meta.Title = newsGet.MetaTitle;
            meta.Keywords = newsGet.Tag;
            meta.Description = newsGet.Spot;
            meta.Image = "https://uploads.drcerenayozger.com/images/" + newsGet.Image;
            meta.ogDescription = newsGet.Spot;
            meta.ogTitle = newsGet.Title;
            meta.ogImage = "https://uploads.drcerenayozger.com/images/" + newsGet.Image;
            meta.Url = "https://uploads.drcerenayozger.com/" + Id + newsGet.Title;
            ViewBag.Meta = meta;

            #endregion    

            if (!string.Equals(friendlyTitle, Title, StringComparison.Ordinal))
            {
                return this.RedirectToRoutePermanent("islem", new { id = Id, title = friendlyTitle });
            }

            return View(newsGet);
        }

        #region Diller

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return LocalRedirect(returnUrl);
        }

        #endregion

        #region Content

        private static string HtmlToPlainText(string Title)
        {
            string phrase = string.Format("{0}", Title);

            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private static string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        #endregion

        #region External

        [HttpPost]
        public async Task<IActionResult> FormGonderIletisim(EmailSenderViewModel model)
        {
            try
            {
                string messages = model.content;
                var message = new Message()
                {
                    To = "info@drcerenayozger.com",
                    Subject = "Başvuru - Dr. Ceren Ayözger",
                    Phone = model.phone,
                    Email = model.email,
                    NameSurname = model.namesurname,
                    Content = $@"<p>{model.namesurname} iletişim formunu doldurdu. (Bu form https://drcerenayozger.com/iletisim üzerinden gelmiştir.) <p> <hr/> <p>Email Adresi: {model.email}</p> <hr/>  <p>Telefon: {model.phone}</p> <hr/> <p>{messages}</p> <hr/>",
                };

                await _emailSender.SendEmailAsync(message);
                string result = "Başvuru formu başarıyla dolduruldu! Sizinle en kısa sürede iletişime geçeceğiz";

                return RedirectToAction("sonuc", "anasayfa", new { result = result, type = 1 });

            }
            catch (Exception ex)
            {
                string result = "Mail gönderilirken bir hata oluştu!";
                return RedirectToAction("sonuc", "anasayfa", new { result = result, type = 0 });
            }

        }

        #endregion

        public IActionResult sonuc(string result, int type)
        {
            ViewBag.Result = result;
            ViewBag.Type = type;

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Çerez Politikası";
            meta.Keywords = "Çocuk, Klinik, Ayözger, Dentist, Bakım";
            meta.Description = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Çocuk Hastalıkları ve Sağlığı Klinik Merkezi";
            meta.ogTitle = "Dr. Ceren Ayözger | Doktor & Dişçi";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerenayozger.com/";
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

    }
}
