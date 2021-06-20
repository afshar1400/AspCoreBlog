using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Comments
{
    public class MainComment:Comment
    {
        List<SubComment> subComments { get; set; }
    }
}
