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
    public class TestResultAnswerRepo : BaseRepo<TestResultAnswer>, ITestResultAnswerRepo
    {
        public TestResultAnswerRepo(SphssContext context) : base(context) { }

        public Task<IEnumerable<TestResultAnswer>> GetTestResultAnswersByTestResultAsync(int testResultId)
        {
            throw new NotImplementedException();
        }

        /*public async Task<IEnumerable<TestResultAnswer>> GetTestResultAnswersByTestResultAsync(int testResultId)
        {
            return await _dbSet
                .Where(tra => tra.TestResultId == testResultId)
                .Include(tra => tra.Question)
                .ToListAsync();
        }*/
    }
}
