using AutoMapper;
using BusinessObject;
using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using DataAccess.Repo.IRepo;
using DataAccess.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class TestResultService : ITestResultService
    {
        private readonly ITestResultRepo _testResultRepo;
        private readonly IMapper _mapper;

        public TestResultService(ITestResultRepo testResultRepo, IMapper mapper)
        {
            _testResultRepo = testResultRepo;
            _mapper = mapper;
        }

        public async Task<ResFormat<ResTestResultDTO>> GetTestResultByStudentAsync(int studentId, int testId)
        {
            var res = new ResFormat<ResTestResultDTO>();
            try
            {
                var testResult = await _testResultRepo.GetTestResultByStudentAsync(studentId, testId);
                if (testResult != null)
                {
                    var resultDTO = _mapper.Map<ResTestResultDTO>(testResult);
                    res.Success = true;
                    res.Data = resultDTO;
                    res.Message = "Test Result Retrieved Successfully";
                }
                else
                {
                    res.Success = false;
                    res.Message = "No Test Result found for the given student and test.";
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed to retrieve Test Result: {ex.Message}";
            }

            return res;
        }

        public async Task<ResFormat<IEnumerable<ResTestResultDTO>>> GetTestResultsByStudentAsync(int studentId)
        {
            var res = new ResFormat<IEnumerable<ResTestResultDTO>>();
            try
            {
                var testResults = await _testResultRepo.GetTestResultsByStudentAsync(studentId);
                var resultDTOs = _mapper.Map<IEnumerable<ResTestResultDTO>>(testResults);
                res.Success = true;
                res.Data = resultDTOs;
                res.Message = "Test Results Retrieved Successfully";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed to retrieve Test Results: {ex.Message}";
            }

            return res;
        }

        public async Task<ResFormat<bool>> AddTestResultAsync(TestResultCreateDTO testResultCreateDTO)
        {
            var res = new ResFormat<bool>();
            try
            {
                var testResult = _mapper.Map<TestResult>(testResultCreateDTO);
                testResult.TestDate = DateTime.Now;
                await _testResultRepo.AddAsync(testResult);
                res.Success = true;
                res.Data = true;
                res.Message = "Test Result Created Successfully";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed to create Test Result: {ex.Message}";
            }

            return res;
        }

        public async Task<ResFormat<bool>> UpdateTestResultAsync(TestResultCreateDTO testResultCreateDTO, int testResultId)
        {
            var res = new ResFormat<bool>();
            try
            {
                var testResult = await _testResultRepo.GetByIdAsync(testResultId);
                if (testResult != null)
                {
                    _mapper.Map(testResultCreateDTO, testResult);
                    _testResultRepo.Update(testResult);
                    res.Success = true;
                    res.Data = true;
                    res.Message = "Test Result Updated Successfully";
                }
                else
                {
                    res.Success = false;
                    res.Message = "Test Result Not Found";
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed to update Test Result: {ex.Message}";
            }

            return res;
        }
    }
}
