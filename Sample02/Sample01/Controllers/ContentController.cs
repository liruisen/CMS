using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sample01.Models;

namespace Sample01.Controllers
{
    public class ContentController : Controller
    {
        private readonly Content contents;
        public ContentController(IOptions<Content> options)
        {
            this.contents = options.Value;
        }
        public IActionResult Index()
        {
            return View(new ContentViewModel() { Contents = new List<Content> { contents } });
        }
    }
}