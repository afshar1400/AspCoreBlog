using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public interface IRepository<T> where T : class
    {
     IEnumerable<T> GetAll(int page,int pageSize);
     IEnumerable<T> GetAll();
     int GetRange();
     T GetById(int id);
     void Add(T entity);
     void Delete(T entity);
    }
}
