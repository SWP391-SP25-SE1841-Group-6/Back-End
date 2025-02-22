using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.IService
{
    public interface ITestQuestionService
    {
        Task<ResFormat<bool>> RemoveTestQuestion(TestQuestionAddDTO testQuestion);
        Task<ResFormat<bool>> AddTestQuestion(TestQuestionAddDTO testQuestion);
    }
}
