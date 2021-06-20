using Blog.Data;
using Blog.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repository.MainCmtRepo
{
    public class MainCommentRepository : Repository<MainComment>, IMainCommentRepository
    {
        public MainCommentRepository(AppDbCtx appDbCtx) : base(appDbCtx)
        {
        }
        public IEnumerable<MainComment> GetAllPostCmts(int postId)
        {
            var cmts = _appDbCtx.MainComments.Where(cm => cm.PostId == postId);
   
            return cmts;
        }
    }
}
