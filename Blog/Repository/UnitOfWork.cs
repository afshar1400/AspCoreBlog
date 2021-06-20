using Blog.Data;
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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbCtx _context;

        public UnitOfWork(AppDbCtx context)
        {
            _context = context;
            Posts = new PostRepository(_context);
            Categorys = new CategoryRepository(_context);
            MainComments = new MainCommentRepository(_context);
            NewsLetter = new NewsLetterRepository(_context);
        }
        public IPostRepository Posts { get; private set; }

        public ICategoryRepository Categorys { get; private set; }

        public IMainCommentRepository MainComments { get; private set; }
        public INewsLetterRepository NewsLetter { get; }

        public int Complete()
        {
           return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
