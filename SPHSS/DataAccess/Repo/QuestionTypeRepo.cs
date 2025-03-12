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
    public class QuestionTypeRepo : BaseRepo<QuestionType>, IQuestionTypeRepo
    {
        private readonly SphssContext _context;
        public QuestionTypeRepo(SphssContext context) : base(context)
        {
            _context = context;
        }

        public async Task<QuestionType> GetQuestionTypeAndQuestionsById(int id)
        {
            return await _context.QuestionTypes.Include(q => q.Questions).FirstOrDefaultAsync(q => q.QtypeId == id && q.IsDeleted == false);
        }

        public async Task<QuestionType> GetQuestionTypeAndQuestionsByType(string type)
        {
            return await _context.QuestionTypes.Include(q=>q.Questions).FirstOrDefaultAsync(q=>q.Qtype.ToLower().Trim().Equals(type.ToLower().Trim()) && q.IsDeleted == false);
        }
    }
}
