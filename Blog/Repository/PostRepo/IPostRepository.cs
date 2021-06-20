using Blog.ModelDto;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository.PostRepo
{
    public interface IPostRepository : IRepository<Post>
    {
        void Update(PostDto postDto,string fileName);

        IEnumerable<Post> GetPostWithPage(int page, int pageSize, string? search,int? categoryId);

        int GetSearchAndCategoryRange(string? search, int? categoryId);
    }
}
