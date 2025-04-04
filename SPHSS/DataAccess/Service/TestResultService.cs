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
        private readonly ITestResultDetailRepo _testResultDetailRepo;

        public TestResultService(ITestResultRepo testResultRepo, 
                                 IMapper mapper, 
                                 ITestQuestionRepo testQuestionRepo,
                                 ITestResultAnswerRepo testResultAnswerRepo,
                                 ITestResultDetailRepo testResultDetailRepo)
        {
            _testResultRepo = testResultRepo;
            _mapper = mapper;
            _testQuestionRepo = testQuestionRepo;
            _testResultAnswerRepo = testResultAnswerRepo;
            _testResultDetailRepo = testResultDetailRepo;
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
        public async Task<ResFormat<IEnumerable<ResTestResultDTO>>> GetTestResultsByStudentIdAsync(int studentId)
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

        public async Task<ResFormat<bool>> AddTestResultAsync(TestResultCreateDTO testResultCreateDTO, int userId)
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
                testResult.StudentId = userId;
                testResult.TestDate = DateTime.Now;
                testResult.IsDeleted = false;
                await _testResultRepo.AddAsync(testResult);
                var newResult = await _testResultRepo.GetByIdAsync(testResult.TestResultId);

                if(newResult!=null)
                {
                    int sum = 0;
                    double avgScore=0;
                    int numberOfAnswer = 0;

                        // Dictionary to store scores by question type
                        var scoreByType = new Dictionary<string, List<int>>();

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

                            // Store scores by question type
                            if (!scoreByType.ContainsKey(newAnswer.Qtype))
                            {
                                scoreByType[newAnswer.Qtype] = new List<int>();
                            }
                            scoreByType[newAnswer.Qtype].Add((int)newAnswer.Answer);

                        }
                        avgScore =(double)sum/numberOfAnswer;
                        newResult.Score = avgScore;
                        _testResultRepo.Update(newResult);

                        // Calculate ScoreType for each Qtype
                        foreach (var qtype in scoreByType.Keys)
                        {
                            var scores = scoreByType[qtype];
                            double scoreTypeAvg = scores.Average(); // Average of 2 questions per Qtype

                            var testResultDetail = new TestResultDetail
                            {
                                TestResultId = testResult.TestResultId,
                                Qtype = qtype,
                                ScoreType = scoreTypeAvg,
                                IsDeleted = false
                            };
                            await _testResultDetailRepo.AddAsync(testResultDetail);
                        }
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
