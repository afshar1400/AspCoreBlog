using Blog.Repository.CategoryRepo;
using Blog.Repository.MainCmtRepo;
using Blog.Repository.NewLetterRepo;
using Blog.Repository.PostRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        IPostRepository Posts { get; }
        ICategoryRepository Categorys { get; }
        IMainCommentRepository MainComments { get; }
        INewsLetterRepository NewsLetter { get; }

        int Complete();
    }
}
