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
            return View();
        }

        [Route("details") ]
        public IActionResult BlogDetails()
        {
            var blogDetails = new BlogDetails()
            {
                Author = "Charles",
                Title = "My blog Post",
                Body = "This is a great blog post",
                PostedOn = DateTime.Now
            };
            return View(blogDetails);
        }
    }
}
