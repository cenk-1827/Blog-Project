using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogContext _blogContext;

        public HomeController(ILogger<HomeController> logger, BlogContext context)
        {
            _logger = logger;
            _blogContext = context;
        }

        public IActionResult Index()
        {
            var list = _blogContext.Blogs.Take(4).Where(a => a.IsPublish).ToList();
            foreach (var blog in list)
            {
                blog.Author = _blogContext.Authot.Find(blog.Authorid);
            }
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Post(int id)
        {
            var blog = _blogContext.Blogs.Find(id);
            blog.Author = _blogContext.Authot.Find(blog.Authorid);
            blog.ImagePath = "/assets/img/" + blog.ImagePath;
            return View(blog);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
