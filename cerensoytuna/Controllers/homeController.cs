﻿using AutoMapper;
using cerensoytuna.COMMON.DTOS.PostDTO.PostLanguageDTO;
using cerensoytuna.COMMON.Helpers;
using cerensoytuna.COMMON.PostDTO;
using cerensoytuna.COMMON.PostDTO.TagPostDTO;
using cerensoytuna.ENGINES.Interface;
using cerensoytuna.Models;
using cerensoytuna.Models.PostLanguageModel;
using cerensoytuna.Models.PostModel;
using cerensoytuna.Models.TagPostModel;
using cerensoytuna.Resource;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cerensoytuna.Controllers
{
    public class homeController : Controller
    {

        private readonly LocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly IEmailSender _emailSender;
        public homeController(LocalizationService localizationService, IMapper mapper, IPostService postService, IEmailSender emailSender)
        {
            _localizationService = localizationService;
            _mapper = mapper;
            _emailSender = emailSender;
            _postService = postService;
        }

        public IActionResult contact()
        {
            string menuName = _localizationService.GetLocalizedHtmlString("İLETİŞİM");

            if (menuName == "İletişim")
            {
                return RedirectToAction("iletisim", "anasayfa");
            }
            else if(menuName == "Kontaktieren Sie uns")
            {
                return RedirectToAction("kontakt", "startseite");
            }

            #region Meta

            MetaViewModel meta = new()
            {
                Title = "Contact Us",
                Keywords = "Child, Clinic, Ayözger, Dentist, Care",
                Description = "Pediatric Diseases and Health Clinic",
                Image = "https://uploads.drcerenayozger.com/site/logodr.png",
                ogDescription = "Pediatric Diseases and Health Clinic",
                ogTitle = "Dr. Ceren Ayözger | Contact",
                ogImage = "https://uploads.drcerenayozger.com/site/logodr.png",
                Url = "https://www.drcerenayozger.com/"
            };
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        public IActionResult booknow()
        {
            string menuName = _localizationService.GetLocalizedHtmlString("RANDEVU");

            if (menuName == "Randevu Al")
            {
                return RedirectToAction("randevual", "anasayfa");
            }

            else if (menuName == "Einen Termin vereinbaren")
            {
                return RedirectToAction("jetztbuchen", "startseite");
            }


            #region Meta

            MetaViewModel meta = new()
            {
                Title = "Book Now",
                Keywords = "Child, Clinic, Ayözger, Dentist, Care",
                Description = "Pediatric Diseases and Health Clinic",
                Image = "https://uploads.drcerenayozger.com/site/logodr.png",
                ogDescription = "Pediatric Diseases and Health Clinic",
                ogTitle = "Dr. Ceren Ayözger | Book Now",
                ogImage = "https://uploads.drcerenayozger.com/site/logodr.png",
                Url = "https://www.drcerenayozger.com/"
            };
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        public IActionResult aboutus()
        {
            string menuName = _localizationService.GetLocalizedHtmlString("KLİNİĞİMİZ");

            if (menuName == "Kliniğimiz")
            {
                return RedirectToAction("hakkimizda", "anasayfa");
            }
            else if (menuName == "Unsere Klinik")
            {
                return RedirectToAction("uberuns", "startseite");
            }


            #region Meta

            MetaViewModel meta = new()
            {
                Title = "About Us",
                Keywords = "Child, Clinic, Ayözger, Dentist, Care",
                Description = "Pediatric Diseases and Health Clinic",
                Image = "https://uploads.drcerenayozger.com/site/logodr.png",
                ogDescription = "Pediatric Diseases and Health Clinic",
                ogTitle = "Dr. Ceren Ayözger | About Us",
                ogImage = "https://uploads.drcerenayozger.com/site/logodr.png",
                Url = "https://www.drcerenayozger.com/"
            };
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        public IActionResult treatments(int? pageNumber)
        {

            string menuName = _localizationService.GetLocalizedHtmlString("TEDAVİLER");

            if (menuName == "Tedaviler")
            {
                return RedirectToAction("islemler", "anasayfa");
            }
            else if (menuName == "Behandlungen")
            {
                return RedirectToAction("behandlungen", "startseite");
            }


            ViewBag.TitlePage = "Treatments";
            ViewBag.SubTitle = "Home";

            ViewBag.LangEng = "English";
            ViewBag.LangTr = "Turkish";
            ViewBag.LangDe = "Germany";

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Treatments";
            meta.Keywords = "Child, Clinic, Ayözger, Dentist, Care";
            meta.Description = "Pediatric Diseases and Health Clinic";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Pediatric Diseases and Health Clinic";
            meta.ogTitle = "Dr. Ceren Ayözger | Treatments";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerenayozger.com/";
            ViewBag.Meta = meta;

            #endregion

            int pageSize = 20;

            var haberlist = _mapper.Map<List<PostListItemDto>, List<PostListViewModel>>(_postService.PostListWithWebEng());
            ViewBag.Gonderiler = PaginationList<PostListViewModel>.Create(haberlist.ToList(), pageNumber ?? 1, pageSize);

            return View();
        }


        [HttpGet("treatment/{Id}/{Title}", Name = "treatment")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult treatment(int Id, string Title)
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
                return this.RedirectToRoutePermanent("treatment", new { id = Id, title = friendlyTitle });
            }

            return View(newsGet);
        }

        public IActionResult ourdoctor()
        {
            string menuName = _localizationService.GetLocalizedHtmlString("DR. CEREN SOYTUNA");

            if (menuName == "Dr. Ceren Ayözger")
            {
                return RedirectToAction("doktorumuz", "anasayfa");
            }
            else if(menuName == "Dtr. Ceren Ayözger")
            {
                return RedirectToAction("unserarzt", "home");
            }

            #region Meta

            MetaViewModel meta = new()
            {
                Title = "Dt. Ceren Ayözger",
                Keywords = "Child, Clinic, Ayözger, Dentist, Care",
                Description = "Pediatric Diseases and Health Clinic",
                Image = "https://uploads.drcerenayozger.com/site/logodr.png",
                ogDescription = "Pediatric Diseases and Health Clinic",
                ogTitle = "Dr. Ceren Ayözger | Our Doctor",
                ogImage = "https://uploads.drcerenayozger.com/site/logodr.png",
                Url = "https://www.drcerenayozger.com/"
            };
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        public IActionResult result()
        {
            string menuName = _localizationService.GetLocalizedHtmlString("SONUC");

            if (menuName == "Sonuç")
            {
                return RedirectToAction("sonuc", "anasayfa");
            }
            else if(menuName == "Ergebnis")
            {
                return RedirectToAction("ergebnis", "startseite");
            }

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Result";
            meta.Keywords = "Child, Clinic, Ayözger, Dentist, Care";
            meta.Description = "Pediatric Diseases and Health Clinic";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Pediatric Diseases and Health Clinic";
            meta.ogTitle = "Dr. Ceren Ayözger | Result";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerenayozger.com/";
            ViewBag.Meta = meta;

            #endregion

            return View();
        }

        /// <summary>
        /// BURADA KALDIM ALMANCASINI VE TÜRKÇESİNİ SEÇİM İF ELSE YAPILACAK
        /// </summary>
        /// <returns></returns>

        public IActionResult cookie()
        {

            #region Meta

            MetaViewModel meta = new MetaViewModel();

            meta.Title = "Cookie Policy";
            meta.Keywords = "Child, Clinic, Ayözger, Dentist, Care";
            meta.Description = "Pediatric Diseases and Health Clinic";
            meta.Image = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.ogDescription = "Pediatric Diseases and Health Clinic";
            meta.ogTitle = "Dr. Ceren Ayözger | Cookie Policy";
            meta.ogImage = "https://uploads.drcerenayozger.com/site/logodr.png";
            meta.Url = "https://www.drcerensoytuna.com/";
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
