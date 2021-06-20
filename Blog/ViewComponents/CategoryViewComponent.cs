using Blog.Models;
using Blog.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewComponents
{
    public class CategoryViewComponent: ViewComponent
    {
        private IUnitOfWork _unitOfWork;

        public CategoryViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
  
            return View(_unitOfWork.Categorys.GetAll());
        }
    }
}
