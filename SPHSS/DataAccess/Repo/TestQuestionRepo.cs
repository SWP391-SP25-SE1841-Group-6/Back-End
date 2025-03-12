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
    public class TestQuestionRepo:BaseRepo<TestQuestion>, ITestQuestionRepo
    {
        private readonly SphssContext _context;
        public TestQuestionRepo(SphssContext context):base(context) 
        {
            _context = context;
        }

        public async Task<QuestionType> GetQtypeOfTestQuestionByTestQuestionId(int id)
        {
            /*return await _context.QuestionTypes.Include(q => q.Questions).ThenInclude(t => t.TestQuestions).FirstOrDefaultAsync();*/
            return await _context.QuestionTypes
            .Include(q => q.Questions)
            .ThenInclude(t => t.TestQuestions)
            .FirstOrDefaultAsync(q => q.Questions.Any(question => question.TestQuestions.Any(tq => tq.TestQuestionId == id)));
        }

        public async Task<IEnumerable<TestQuestion>> GetQuestions()
        {
            return await _context.TestQuestions.Include(t=>t.Question).ToListAsync();
        }
    }
}
