using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repo.IRepo
{
    public interface ITestQuestionRepo: IBaseRepo<TestQuestion>
    {
        Task<IEnumerable<TestQuestion>> GetQuestions();
        Task<QuestionType> GetQtypeOfTestQuestionByTestQuestionId(int id);
    }
}
