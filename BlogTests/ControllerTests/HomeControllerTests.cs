using Blog.Controllers;
using Blog.ModelDto;
using Blog.Models;
using Blog.Repository;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlogTests.ControllerTests
{
    public class HomeControllerTests
    {
  



        [Fact]
        public void Index_ReturnViewModel_withListOf5posts()
        {
           var  mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.Posts.GetAll()).Returns(new List<Post> {
                new Post {Id=1,Title="1",Body="1" } ,
                new Post {Id=2,Title="2",Body="2" } ,
                new Post {Id=3,Title="3",Body="3" } ,
            });


            var index = new HomeController(mockRepo.Object).Index();

            var viewResult = Assert.IsType<ViewResult>(index);
            var model = Assert.IsAssignableFrom<BlogIndexViewModel>(viewResult.ViewData.Model);
           
        }

        [Fact]
        public void Create_ReturnViewHasFormHttpGet()
        {
          var mockRepo = new Mock<IUnitOfWork>();

            mockRepo.Setup(repo => repo.Categorys.GetAll()).Returns(new List<Category> {
                new Category {Id=1,Title="1",body="1", } ,
                new Category  {Id=2,Title="2",body="2" } ,
                new Category {Id=3,Title="3",body="3" } ,
            });
            var create = new HomeController(mockRepo.Object).Create();

             Assert.IsType<ViewResult>(create);
        }

        [Fact]
        public void Update_findPostById_returnsToViewModel()
        {
           var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.Posts.GetById(1)).Returns(
                new Post { Id = 1, Title = "1", Body = "1" });
            mockRepo.Setup(x => x.Categorys.GetAll()).Returns(new List<Category> {
             new Category{Id=1,Title="title 1",body="category 1" },
             new Category{Id=2,Title="title 2",body="category 2" },
            });
            var update = new HomeController(mockRepo.Object).Update(1);
            
            
            var result = Assert.IsType<ViewResult>(update);
            var model = Assert.IsAssignableFrom<PostDto>(result.ViewData.Model);

        }
        [Fact]
        public void Update_canNotFindPostById_redirectToIndex()
        {
           var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(x=> x.Posts.GetById(1)).Returns<Post>(null);

            var update = new HomeController(mockRepo.Object).Update(1);

            var result = Assert.IsType<RedirectToActionResult>(update);
             Assert.Equal("Index", result.ActionName);

        }


        [Fact]
        public void Delete_IdEnteredHasAPost()
        {
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(x => x.Posts.GetById(1)).Returns(
                new Post { Id = 1, Title = "1", Body = "1" });

            var delete= new HomeController(mockRepo.Object).Delete(1);

            var result= Assert.IsType<RedirectToActionResult>(delete);
            Assert.Equal("Index", result.ActionName);

        }
        [Fact]
        public void Delete_IdEnteredIsNotValid()
        {
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(x => x.Posts.GetById(2)).Returns<Post>(null);

            var delete= new HomeController(mockRepo.Object).Delete(1);

            var result= Assert.IsType<NotFoundResult>(delete);
          

        }
    }
}
