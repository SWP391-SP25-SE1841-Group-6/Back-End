using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repo.IRepo
{
    public interface IQuestionRepo:IBaseRepo<Question>
    {
        Task<IEnumerable<Question>> GetAllQuestionsWithType();
    }
}
