using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample01.Models;

namespace Sample01.Controllers
{
    public class ContentController : Controller
    {
        public IActionResult Index()
        {
            var contents = new List<Content>();
            for (int i = 0; i < 10; i++)
            {
                contents.Add(new Models.Content()
                {
                    id = i,
                    title = $"{i}的标题",
                    content = $"{i}的正文",
                    status = 1,
                    add_time = DateTime.Now.AddDays(-i),
                });
            }
            return View(new ContentViewModel() { Contents=contents});
        }
    }
}