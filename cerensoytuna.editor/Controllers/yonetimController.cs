using AutoMapper;
using cerensoytuna.ENGINES.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cerensoytuna.editor.Controllers
{
    public class yonetimController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPostService _newService;

        public yonetimController(IMapper mapper, IPostService newService)
        {
            _mapper = mapper;
            _newService = newService;
        }

        [Authorize]
        public IActionResult anasayfa()
        {
            return View();
        }

        [Route("/yonetim/hata/{code:int}")]
        public IActionResult hata(int code)
        {
            ViewData["ErrorCode"] = $"{code}";
            return View("~/Views/yonetim/hata.cshtml");
        }

    }
}
