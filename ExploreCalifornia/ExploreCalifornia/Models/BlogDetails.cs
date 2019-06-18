using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Models
{
    public class BlogDetails
    {
        public string Title { get; set; }
        public DateTime PostedOn { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
    }
}
