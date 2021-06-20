using Blog.ModelDto;
using Blog.Models.Comments;
using Blog.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "user,admin")]
        public IActionResult CreateComment(CommentDto cmtDto)
        {
            if (ModelState.IsValid)
            {
                MainComment mainCmt = new MainComment { Title=cmtDto.Title,Body=cmtDto.body,PostId=cmtDto.PostId };
                _unitOfWork.MainComments.Add(mainCmt);
                _unitOfWork.Complete();
                var allCmts=GetPostCmts(cmtDto.PostId);
                return PartialView("_cmtList", allCmts);
            }
            return StatusCode(500, "Internal server error");
        }

        public IEnumerable<MainComment> GetPostCmts(int postId)
        {
            var post = _unitOfWork.Posts.GetById(postId);



            var cmts = _unitOfWork.MainComments.GetAllPostCmts(post.Id);
    

              return cmts;

            
        }
        [Authorize(Roles = "admin")]
        public void DeleteComment(int cmtId)
        {
            var cmt = _unitOfWork.MainComments.GetById(cmtId);
            _unitOfWork.MainComments.Delete(cmt);
            _unitOfWork.Complete();
        }
    }
}
