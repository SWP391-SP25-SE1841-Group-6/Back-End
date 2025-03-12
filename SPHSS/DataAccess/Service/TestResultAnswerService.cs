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
    public class TestResultAnswerService : ITestResultAnswerService
    {
        private readonly ITestResultAnswerRepo _testResultAnswerRepo;
        private readonly IMapper _mapper;
        private readonly ITestResultRepo _testResultRepo;  
        private readonly IQuestionRepo _questionRepo;

        public TestResultAnswerService(ITestResultAnswerRepo testResultAnswerRepo, IMapper mapper,
            ITestResultRepo testResultRepo, IQuestionRepo questionRepo)
        {
            _testResultAnswerRepo = testResultAnswerRepo;
            _mapper = mapper;
            _testResultRepo = testResultRepo;
            _questionRepo = questionRepo;
        }

        public Task<ResFormat<bool>> AddTestResultAnswerAsync(TestResultAnswerCreateDTO testResultAnswerCreateDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ResFormat<IEnumerable<ResTestResultAnswerDTO>>> GetTestResultAnswersByTestResultAsync(int testResultId)
        {
            var res = new ResFormat<IEnumerable<ResTestResultAnswerDTO>>();
            try
            {
                var testResultAnswers = await _testResultAnswerRepo.GetTestResultAnswersByTestResultAsync(testResultId);
                var resultDTOs = _mapper.Map<IEnumerable<ResTestResultAnswerDTO>>(testResultAnswers);
                res.Success = true;
                res.Data = resultDTOs;
                res.Message = "Test Result Answers Retrieved Successfully";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed to retrieve Test Result Answers: {ex.Message}";
            }

            return res;
        }

        /*public async Task<ResFormat<bool>> AddTestResultAnswerAsync(TestResultAnswerCreateDTO testResultAnswerCreateDTO)
        {
            var res = new ResFormat<bool>();
            try
            {
                var existQuestion = await _questionRepo.GetByIdAsync(testResultAnswerCreateDTO.TestQuestionId);
                var existTestResult = await _testResultRepo.GetByIdAsync(testResultAnswerCreateDTO.TestResultId);
                if (existTestResult==null||existQuestion==null)
                {
                    res.Success = false;
                    res.Message = "No Result/Question existed";
                }
                else
                {
                    var testResultAnswer = _mapper.Map<TestResultAnswer>(testResultAnswerCreateDTO);
                    await _testResultAnswerRepo.AddAsync(testResultAnswer);
                    res.Success = true;
                    res.Data = true;
                    res.Message = "Test Result Answer Created Successfully";
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed to create Test Result Answer: {ex.Message}";
            }

            return res;
        }*/
    }
}
