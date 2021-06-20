using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Comments
{
    public class Comment
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(300)]
        public string Body { get; set; }
        public DateTime Created { get; set; } = DateTime.Today;

        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
