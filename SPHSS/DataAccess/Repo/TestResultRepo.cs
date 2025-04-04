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
    public class TestResultRepo : BaseRepo<TestResult>, ITestResultRepo
    {
        private readonly SphssContext _context;

        public TestResultRepo(SphssContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TestResult?> GetTestResultByStudentAsync(int studentId, int testId)
        {
            return await _context.TestResults
                .Include(tr => tr.TestResultAnswers)
                .ThenInclude(tra => tra.TestQuestion)
                .ThenInclude(q => q.Question)
                .ThenInclude(q => q.Qtype)
                .Include(tr => tr.TestResultDetails)
                .FirstOrDefaultAsync(tr => tr.StudentId == studentId && tr.TestId == testId && !(bool)tr.IsDeleted);
        }

        public async Task<IEnumerable<TestResult>> GetTestResultsByStudentAsync(int studentId)
        {
            return await _context.TestResults
                .Include(tr => tr.TestResultAnswers)
                .ThenInclude(tra => tra.TestQuestion)
                .ThenInclude(q => q.Question)
                .ThenInclude(q => q.Qtype)
                .Include(tr => tr.TestResultDetails)
                .Where(tr => tr.StudentId == studentId && !(bool)tr.IsDeleted)
                .ToListAsync();
        }
        public async Task<Test?> GetNewestTestAsync()
        {
            return await _context.Tests
                .Where(t => !(bool)t.IsDeleted)
                .OrderByDescending(t => t.DateCreated)
                .FirstOrDefaultAsync();
        }
    }
}
