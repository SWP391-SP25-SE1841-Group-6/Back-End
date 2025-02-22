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
    public class TestRepo : BaseRepo<Test>, ITestRepo
    {
        private readonly SphssContext _context;
        public TestRepo(SphssContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Test> GetTestAndTestQuestionsById(int id)
        {
            return await _context.Tests.Include(q => q.TestQuestions)
                .ThenInclude(q=>q.Question).ThenInclude(q=>q.Qtype)
                .FirstOrDefaultAsync(t => t.TestId == id && t.IsDeleted == false);
        }
    }
}
