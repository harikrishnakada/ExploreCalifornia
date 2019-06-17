using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [Route("blogPost") ]
        public IActionResult Post()
        {
            ViewBag.Title = "My blog Post";
            ViewBag.Posted = DateTime.Now;
            ViewBag.Author = "Charles";
            ViewBag.Body = "This is a great blog post";

            return View();
        }
    }
}
