using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(500)]
        public string body { get; set; }
        public List<Post> Posts { get; set; }
    }
}