using Blog.ModelDto;
using Blog.Models;
using Blog.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Authorize(Roles="admin")]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var categories = _unitOfWork.Categorys.GetAll();
            return View(categories);
        }
        [HttpPost]
        public IActionResult Create(TitleAndBody titleBody)
        {
            Category category = new Category { Title = titleBody.Title, body = titleBody.body };
            _unitOfWork.Categorys.Add(category);
            _unitOfWork.Complete();
            return PartialView("Create", _unitOfWork.Categorys.GetAll());
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var catgory = _unitOfWork.Categorys.GetById(id);
            _unitOfWork.Categorys.Delete(catgory);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
