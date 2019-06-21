using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttributeRouting;
using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExploreCalifornia.Controllers
{
    public class BlogController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var posts = new[]
            {
            new BlogDetails{
                Author = "Charles",
                Title = "Finding Charles",
                Body = "This is a great blog post",
                PostedOn = DateTime.Now
            },
            new BlogDetails{
                Author = "Boston",
                Title = "The  new boston",
                Body = "This is a great blog post",
                PostedOn = DateTime.Now
            }
        };

            return View(posts);
    }

    [Route("details")]
    public IActionResult BlogDetails()
    {
            return View();
    }
}
}
