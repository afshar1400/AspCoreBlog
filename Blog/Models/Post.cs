using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(2000)]
        public string Body { get; set; }
        [StringLength(500)]
        public string ImageName { get; set; }
        public DateTime Created { get; set; } = DateTime.Today;

        public int CategroyId { get; set; } 
        public Category Categroy { get; set; }
    }
}
