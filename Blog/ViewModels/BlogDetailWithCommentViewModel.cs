using Blog.Models;
using Blog.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class BlogDetailWithCommentViewModel
    {
        public Post post { get; set; }
        public IEnumerable<MainComment> mainComments{ get; set; }
    }
}
