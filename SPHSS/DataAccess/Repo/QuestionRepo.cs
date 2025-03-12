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
        private readonly SphssContext _context;
        public QuestionRepo(SphssContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Question>> GetAllQuestionsWithType()
        {
            return await _context.Questions.Include(qt => qt.Qtype).Where(q =>q.IsDeleted == false).ToListAsync();
        }

        public async Task<Question> GetQuestionByIdWithType(int id)
        {
            return await _context.Questions.Include(qt=>qt.Qtype).FirstOrDefaultAsync(q => q.QuestionId==id && q.IsDeleted ==false);
        }
    }
}
