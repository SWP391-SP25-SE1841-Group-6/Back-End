using AutoMapper;
using BusinessObject;
using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using DataAccess.Repo;
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
        private readonly ITestQuestionRepo _testQuestionRepo;
        private readonly ITestResultAnswerRepo _testResultAnswerRepo;

        public TestResultService(ITestResultRepo testResultRepo, 
                                 IMapper mapper, 
                                 ITestQuestionRepo testQuestionRepo,
                                 ITestResultAnswerRepo testResultAnswerRepo)
        {
            _testResultRepo = testResultRepo;
            _mapper = mapper;
            _testQuestionRepo = testQuestionRepo;
            _testResultAnswerRepo = testResultAnswerRepo;
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
                if (testResultCreateDTO.Answers.Any(c=>c.Answer==0||c.Answer==null))
                {
                    res.Success = false;
                    res.Data = false;
                    res.Message = "There is/are question(s) you did not answer";
                }
                else {
                var testResult = _mapper.Map<TestResult>(testResultCreateDTO);
                testResult.TestDate = DateTime.Now;
                testResult.IsDeleted = false;
                await _testResultRepo.AddAsync(testResult);
                var newResult = await _testResultRepo.GetByIdAsync(testResult.TestResultId);
                if(newResult!=null)
                {
                    int sum = 0;
                    double avgScore=0;
                    int numberOfAnswer = 0;
                    foreach (var answer in testResultCreateDTO.Answers)
                    {
                        var existTQ = await _testQuestionRepo.GetQtypeOfTestQuestionByTestQuestionId(answer.TestQuestionId);
                        var newAnswer = new TestResultAnswer
                        {
                            TestResultId=testResult.TestResultId,
                            TestQuestionId=answer.TestQuestionId,
                            Answer=answer.Answer,
                            Qtype=existTQ.Qtype,
                            IsDeleted=false,
                        };
                        await _testResultAnswerRepo.AddAsync(newAnswer);
                        sum+=(int)newAnswer.Answer;
                        numberOfAnswer++;
                    }
                    avgScore =(double)sum/numberOfAnswer;
                    newResult.Score = avgScore;
                    _testResultRepo.Update(newResult);
                }
                res.Success = true;
                res.Data = true;
                res.Message = "Test Result Created Successfully";
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed to create Test Result: {ex.Message}";
            }

            return res;
        }
    }
}
