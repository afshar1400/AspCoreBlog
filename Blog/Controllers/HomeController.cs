using Blog.ModelDto;
using Blog.Models;
using Blog.Models.Comments;
using Blog.Repository;
using Blog.Repository.PostRepo;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [AllowAnonymous]
        public IActionResult Index(int page = 1, int Size = 5, string search = null, int categoryId=0 )
        {

            int range = _unitOfWork.Posts.GetSearchAndCategoryRange(search,categoryId);
            int sizeCheck= range/ Size;
            int pageNumber = range % Size == 0 ? sizeCheck : sizeCheck + 1;

            IEnumerable<Post> posts = _unitOfWork.Posts.GetPostWithPage(page,Size,search,categoryId);

           
            BlogIndexViewModel blogIndexViewModel = new BlogIndexViewModel {PageIndex=page,PageSize=Size,Posts=posts ,NumberOfPages=pageNumber,search=search};
           
            return View(blogIndexViewModel);
        }

        public IActionResult Create()
        {
            var postDto = new PostDto { categories=_unitOfWork.Categorys.GetAll().ToList(),};
            return View(postDto);
        }

        public IActionResult Update(int id)
        {
            var post = _unitOfWork.Posts.GetById(id);
            if (post == null)
                return RedirectToAction("Index");

            var postDto = new PostDto {Id=post.Id,Title=post.Title,Body=post.Body,categoryId=post.CategroyId,categories = _unitOfWork.Categorys.GetAll().ToList()};

            return View("Create",postDto);

        }
        [HttpPost]
        public IActionResult Upsert(PostDto postDto)
        {
            string imageName = "";
            if(postDto.Image !=null )
            {
              imageName = Guid.NewGuid().ToString() + postDto.Image.FileName;
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images",imageName);

              using(var stream=new FileStream(imagePath,FileMode.Create))
              {
                postDto.Image.CopyTo(stream);
              }
            }
            
            
           
            if (postDto.Id >0)
            {
                _unitOfWork.Posts.Update( postDto,imageName);
            }
            else
            {
                Post post = new Post { Body = postDto.Body, Title = postDto.Title, ImageName = imageName,CategroyId=postDto.categoryId };

                _unitOfWork.Posts.Add(post);
            }
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var post = _unitOfWork.Posts.GetById(id);

            if (post == null)
                return NotFound();

            _unitOfWork.Posts.Delete(post);
            _unitOfWork.Complete();
            return RedirectToAction("Index");

        }
        [AllowAnonymous]
        public IActionResult Detail(int id)
        {
            var post = _unitOfWork.Posts.GetById(id);

            if (post == null)
                return RedirectToAction("Index");

            IEnumerable<MainComment> cmts = _unitOfWork.MainComments.GetAllPostCmts(post.Id);

            BlogDetailWithCommentViewModel detail = new BlogDetailWithCommentViewModel { post = post ,mainComments=cmts};
            

;            return View(detail);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
