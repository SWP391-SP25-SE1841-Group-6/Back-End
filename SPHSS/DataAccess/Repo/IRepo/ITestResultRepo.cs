using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repo.IRepo 
{
    public interface ITestResultRepo : IBaseRepo<TestResult>
    {
        Task<TestResult?> GetTestResultByStudentAsync(int studentId, int testId);
        Task<IEnumerable<TestResult>> GetTestResultsByStudentAsync(int studentId);
    }
}
