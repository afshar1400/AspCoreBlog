using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class BlogIndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int NumberOfPages { get; set; }

        public string search { get; set; }
    }
}
