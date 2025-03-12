using BusinessObject;
using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.IService
{
    public interface ITestResultAnswerService
    {
        Task<ResFormat<IEnumerable<ResTestResultAnswerDTO>>> GetTestResultAnswersByTestResultAsync(int testResultId);
        Task<ResFormat<bool>> AddTestResultAnswerAsync(TestResultAnswerCreateDTO testResultAnswerCreateDTO);
    }
}
