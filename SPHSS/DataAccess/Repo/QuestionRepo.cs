using BusinessObject;
using DataAccess.Repo.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repo
{
    public class QuestionRepo : BaseRepo<Question>, IQuestionRepo
    {
        protected readonly SphssContext _context;
        public QuestionRepo(SphssContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Question> GetQuestionByType(string type)
        {
            return null;
        }
    }
}
