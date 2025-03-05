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
    public interface ITestResultService
    {
        Task<ResFormat<ResTestResultDTO>> GetTestResultByStudentAsync(int studentId, int testId);
        Task<ResFormat<IEnumerable<ResTestResultDTO>>> GetTestResultsByStudentAsync(int studentId);
        Task<ResFormat<bool>> AddTestResultAsync(TestResultCreateDTO testResultCreateDTO);
        Task<ResFormat<bool>> UpdateTestResultAsync(TestResultCreateDTO testResultCreateDTO, int testResultId);
    }
}
