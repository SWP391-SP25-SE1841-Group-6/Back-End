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
    public interface ITestResultDetailService
    {
        Task<ResFormat<IEnumerable<ResTestResultDetailDTO>>> GetTestResultDetailsByTestResultAsync(int testResultId);
        Task<ResFormat<bool>> AddTestResultDetailAsync(TestResultDetailCreateDTO testResultDetailCreateDTO);
    }
}
