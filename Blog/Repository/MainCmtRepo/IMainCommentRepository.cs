using Blog.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository.MainCmtRepo
{
     public interface IMainCommentRepository:IRepository<MainComment>
    {
        IEnumerable<MainComment> GetAllPostCmts(int postId);
    }
}
