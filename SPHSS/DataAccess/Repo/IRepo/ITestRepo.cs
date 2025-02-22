using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repo.IRepo
{
    public interface ITestRepo : IBaseRepo<Test>
    {
        Task<Test> GetTestAndTestQuestionsById(int id);
    }
}
