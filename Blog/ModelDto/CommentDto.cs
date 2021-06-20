using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ModelDto
{
    public class CommentDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string body { get; set; }
        [Required]
        public int PostId { get; set; }
    }
}
