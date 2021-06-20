using Blog.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbCtx _appDbCtx;
        protected readonly DbSet<T> _table;

        public Repository(AppDbCtx appDbCtx)
        {
            _appDbCtx = appDbCtx;
            _table = _appDbCtx.Set<T>();
        }
        public void Add(T entity)
        {
            _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public IEnumerable<T> GetAll(int page,int pageSize)
        {
            var skip = (page - 1) * pageSize;
            IEnumerable<T> all = _table.Skip(skip).Take(pageSize).ToList();
            return all;
            
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> all = _table.ToList();
            return all;
        }


        public T GetById(int id)
        {
            T founded = _table.Find(id);
            if (founded == null)
                return null;
            return founded;
        }

        public int GetRange()
        {
            return _table.Count();
        }
    }
}
