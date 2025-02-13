using AutoMapper;
using cerensoytuna.COMMON.DTOS.PostDTO.PostLanguageDTO;
using cerensoytuna.COMMON.Helpers;
using cerensoytuna.COMMON.PostDTO.TagPostDTO;
using cerensoytuna.COMMON.PostDTO;
using cerensoytuna.ENGINES.Interface;
using cerensoytuna.Models.PostModel;
using cerensoytuna.Models;
using cerensoytuna.Resource;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using cerensoytuna.Models.TagPostModel;
using cerensoytuna.Models.PostLanguageModel;
using System.Linq;

namespace cerensoytuna.Controllers
{
    public class startseiteController : Controller
    {
        private readonly LocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly IEmailSender _emailSender;
        public startseiteController(LocalizationService localizationService, IMapper mapper, IPostService postService, IEmailSender emailSender)
        {
            _localizationService = localizationService;
            _mapper = mapper;
            _emailSender = emailSender;
            _postService = postService;
        }

        public IActionResult kontakt()
        {
            string menuName = _localizationService.GetLocalizedHtmlString("İLETİŞİM");

            if (menuName == "İletişim")
            {
                return RedirectToAction("iletisim", "anasayfa");
            }
            else if(menuName == "Contact")
            {
                return RedirectToAction("contact", "anasayfa");
            }

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Kontakt Üns";
            meta.Keywords = "Kinder, Klinik, Ayözger, Zahnarzt, Pflege";
            meta.Description = "Kinderkrankheiten und Gesundheitsklinik";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Kinderkrankheiten und Gesundheitsklinik";
            meta.ogTitle = "Dr. Ceren Ayözger | Kontakt";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerenayozger.com/";
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        public IActionResult jetztbuchen()
        {
            string menuName = _localizationService.GetLocalizedHtmlString("RANDEVU");

            if (menuName == "Randevu Al")
            {
                return RedirectToAction("randevual", "anasayfa");
            }
            if (menuName == "Make An Appoinment")
            {
                return RedirectToAction("booknow", "home");
            }

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Jetzt buchen";
            meta.Keywords = "Kinder, Klinik, Ayözger, Zahnarzt, Pflege";
            meta.Description = "Kinderkrankheiten und Gesundheitsklinik";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Kinderkrankheiten und Gesundheitsklinik";
            meta.ogTitle = "Dr. Ceren Ayözger | Jetzt Buchen";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerenayozger.com/";
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        public IActionResult uberuns()
        {
            string menuName = _localizationService.GetLocalizedHtmlString("KLİNİĞİMİZ");

            if (menuName == "Kliniğimiz")
            {
                return RedirectToAction("hakkimizda", "anasayfa");
            }
            else if(menuName == "Our Clinic")
            {
                return RedirectToAction("aboutus", "home");
            }

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Über uns";
            meta.Keywords = "Kinder, Klinik, Ayözger, Zahnarzt, Pflege";
            meta.Description = "Kinderkrankheiten und Gesundheitsklinik";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Kinderkrankheiten und Gesundheitsklinik";
            meta.ogTitle = "Dr. Ceren Ayözger | Uber Üns";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerenayozger.com/";
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        public IActionResult behandlungen(int? pageNumber)
        {

            string menuName = _localizationService.GetLocalizedHtmlString("TEDAVİLER");

            if (menuName == "Tedaviler")
            {
                return RedirectToAction("islemler", "anasayfa");
            }
            else if(menuName == "Treatments")
            {
                return RedirectToAction("treatments", "home");
            }

            ViewBag.TitlePage = "Behandlungen";
            ViewBag.SubTitle = "Startseite";

            ViewBag.LangEng = "English";
            ViewBag.LangTr = "Turkish";
            ViewBag.LangDe = "Deutschland";

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Behandlungen";
            meta.Keywords = "Kinder, Klinik, Ayözger, Zahnarzt, Pflege";
            meta.Description = "Kinderkrankheiten und Gesundheitsklinik";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Kinderkrankheiten und Gesundheitsklinik";
            meta.ogTitle = "Dr. Ceren Ayözger | Behandlungen";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerenayozger.com/";
            ViewBag.Meta = meta;

            #endregion

            int pageSize = 20;

            var haberlist = _mapper.Map<List<PostListItemDto>, List<PostListViewModel>>(_postService.PostListWithWebEng());
            ViewBag.Gonderiler = PaginationList<PostListViewModel>.Create(haberlist.ToList(), pageNumber ?? 1, pageSize);

            return View();
        }

        [HttpGet("behandlung/{Id}/{Title}", Name = "behandlung")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult behandlung(int Id, string Title)
        {
            var newsGet = _mapper.Map<PostDto, PostEditViewModel>(_postService.getPost(Id));
            string friendlyTitle = Title;

            if (_localizationService.GetLocalizedHtmlString("Post") == "Yazı")
            {
                var postLanguage = _mapper.Map<PostLanguageDto, EditPostLanguageViewModel>(_postService.getPostLanguage(newsGet.Title));
                var newsGetToLanguage = _mapper.Map<PostDto, PostEditViewModel>(_postService.getPostToTitle(postLanguage.postTrTitle));
                return RedirectToAction("islem", "anasayfa", new { Id = newsGetToLanguage.Id, Title = HtmlToPlainText(newsGetToLanguage.Title) });
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
                return this.RedirectToRoutePermanent("behandlung", new { id = Id, title = friendlyTitle });
            }

            return View(newsGet);
        }

        public IActionResult unserarzt()
        {
            string menuName = _localizationService.GetLocalizedHtmlString("DR. CEREN SOYTUNA");

            if (menuName == "Dr. Ceren Ayözger")
            {
                return RedirectToAction("doktorumuz", "anasayfa");
            }
            else if(menuName == "Drt. Ceren Ayözger")
            {
                return RedirectToAction("ourdoctor", "home");
            }

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Dt. Ceren Ayözger";
            meta.Keywords = "Kinder, Klinik, Ayözger, Zahnarzt, Pflege";
            meta.Description = "Kinderkrankheiten und Gesundheitsklinik";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Kinderkrankheiten und Gesundheitsklinik";
            meta.ogTitle = "Dr. Ceren Ayözger | Unser Arzt";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerenayozger.com/";
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        public IActionResult ergebnis()
        {
            string menuName = _localizationService.GetLocalizedHtmlString("SONUC");

            if (menuName == "Sonuç")
            {
                return RedirectToAction("sonuc", "anasayfa");
            }
            else if(menuName == "Result")
            {
                return RedirectToAction("result", "home");
            }

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Ergebnis";
            meta.Keywords = "Kinder, Klinik, Ayözger, Zahnarzt, Pflege";
            meta.Description = "Kinderkrankheiten und Gesundheitsklinik";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Kinderkrankheiten und Gesundheitsklinik";
            meta.ogTitle = "Dr. Ceren Ayözger | Ergebnis";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerenayozger.com/";
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

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
        //private static string HtmlToPlainText(string html)
        //{
        //    const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
        //    const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
        //    const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
        //    var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
        //    var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
        //    var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

        //    var text = html;
        //    //Decode html specific characters
        //    text = System.Net.WebUtility.HtmlDecode(text);
        //    //Remove tag whitespace/line breaks
        //    text = tagWhiteSpaceRegex.Replace(text, "><");
        //    //Replace <br /> with line breaks
        //    text = lineBreakRegex.Replace(text, Environment.NewLine);
        //    //Strip formatting
        //    text = stripFormattingRegex.Replace(text, string.Empty);

        //    return text;
        //}

    }
}
