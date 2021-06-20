using AutoMapper;
using Blog.Data;
using Blog.ModelDto;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository.PostRepo
{
    public class PostRepository:Repository<Post>,IPostRepository
    {
        public PostRepository(AppDbCtx context):base(context)
        {

        }

        public IEnumerable<Post> GetPostWithPage(int page, int pageSize, string? search,int? categoryId)
        {
            var skip = (page - 1) * pageSize;
            IEnumerable<Post> all = _appDbCtx.Posts.OrderBy(c=> c.Created).Skip(skip).Take(pageSize).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                all = all.Where(s => s.Title.Contains(search));
            }
            if (categoryId !=null&&categoryId>0)
            {
                all = all.Where(s => s.CategroyId == categoryId);
            }
            return all;
        }

        public void Update(PostDto postDto,string fileName)
        {
            var post = _appDbCtx.Posts.Find(postDto.Id);
            post.Body = postDto.Body;
            post.Title = postDto.Title;
            post.Created = DateTime.Today;
            post.CategroyId = postDto.categoryId;
            if (fileName.Length>5)
            {
            post.ImageName = fileName;

            }
 
        }

        public int GetSearchAndCategoryRange(string? search, int? categoryId)
        {
            IEnumerable<Post> all = _appDbCtx.Posts.OrderBy(c => c.Created);
            if (!string.IsNullOrEmpty(search))
            {
                all = all.Where(s => s.Title.Contains(search));
            }
            if (categoryId != null && categoryId > 0)
            {
                all = all.Where(s => s.CategroyId == categoryId);
            }
            return all.Count();
        }

    }
}
