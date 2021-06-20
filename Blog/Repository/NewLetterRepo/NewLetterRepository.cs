using Blog.Data;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.NewLetterRepo
{
    public class NewsLetterRepository : Repository<NewsLetter>, INewsLetterRepository
    {
        public NewsLetterRepository(AppDbCtx appDbCtx) : base(appDbCtx)
        {
        }
    }
}
