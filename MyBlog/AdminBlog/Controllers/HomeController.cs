using AdminBlog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdminBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogContext _context;

        public HomeController(ILogger<HomeController> logger,BlogContext context)
        {
            _logger = logger;
            _context = context;
        }
        //00000000000000000000000000000000000        CATEGORY        0000000000000000000000000000000000000000000000000000000
        public async Task<IActionResult> AddCategory(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Category));
        }

        public IActionResult Category()
        {
            List<Category> List = _context.Categories.ToList();
            return View(List);
        }

        public async Task<IActionResult> DeleteCategory(int? id)
        {
            Category category = await _context.Categories.FindAsync(id);
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Category));
        }
        //000000000000000000000000000000        AUTHOR        0000000000000000000000000000000000000000000000000000000000000000
        public async Task<IActionResult> AddAuthor(Author author)
        {
            await _context.AddAsync(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Author));
        }

        public IActionResult Author()
        {
            List<Author> List = _context.Authot.ToList();
            return View(List);
        }

        public async Task<IActionResult> DeleteAuthor(int? id)
        {
            Author author = await _context.Authot.FindAsync(id);
            _context.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Author));
        }
        //000000000000000000000000000000        LOGIN        00000000000000000000000000000000000000000000000000000000000000000

        public IActionResult Login(string Email, string Password)
        {
            var author = _context.Authot.FirstOrDefault(w => w.Email == Email && w.Password == Password);
            if(author == null)
            {
                return RedirectToAction(nameof(Login));
            }
            HttpContext.Session.SetInt32("id", author.Id);
            return RedirectToAction(nameof(Category));
        }
        //000000000000000000000000000000        INDEX        00000000000000000000000000000000000000000000000000000000000000000

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
