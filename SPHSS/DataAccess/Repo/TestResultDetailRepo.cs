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
    public class TestResultDetailRepo : BaseRepo<TestResultDetail>, ITestResultDetailRepo
    {
        public TestResultDetailRepo(SphssContext context) : base(context) { }

        public async Task<IEnumerable<TestResultDetail>> GetTestResultDetailsByTestResultAsync(int testResultId)
        {
            return await _dbSet
                .Where(trd => trd.TestResultId == testResultId)
                .ToListAsync();
        }
    }
}
