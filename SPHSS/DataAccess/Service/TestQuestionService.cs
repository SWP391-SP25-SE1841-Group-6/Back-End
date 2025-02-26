using AutoMapper;
using BusinessObject;
using DataAccess.DTO.Req;
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
    public class TestQuestionService : ITestQuestionService
    {
        private readonly ITestQuestionRepo _testQuestionRepo;
        private readonly IMapper _mapper;
        private readonly IBaseRepo<Test> _testRepo;
        private readonly IBaseRepo<Question> _questionRepo;
        private readonly IBaseRepo<QuestionType> _questionTypeRepo;

        public TestQuestionService(ITestQuestionRepo testQuestionRepo, IMapper mapper, IBaseRepo<Test> testRepo, IBaseRepo<Question> questionRepo, IBaseRepo<QuestionType> questionTypeRepo)
        {
            _testQuestionRepo = testQuestionRepo;
            _mapper = mapper;
            _testRepo = testRepo;
            _questionRepo = questionRepo;
            _questionTypeRepo = questionTypeRepo;
        }
        /*public async Task<ResFormat<bool>> AddTestQuestion(TestQuestionAddDTO testQuestion)
        {
            var res = new ResFormat<bool>();
            try
            {
                var test = await _testRepo.GetByIdAsync(testQuestion.TestId);
                var question = await _questionRepo.GetByIdAsync(testQuestion.QuestionId);
                if (test != null || question !=null)
                {
                    var list= await _testQuestionRepo.GetAllAsync();
                    if (list.Any(t => t.TestId == testQuestion.TestId && t.QuestionId == testQuestion.QuestionId))
                    {
                        res.Success = false;
                        res.Message = "Question already exist in Test";
                        return res;
                    }
                    else 
                    {
                        var mapp = _mapper.Map<TestQuestion>(testQuestion);
                        mapp.DateAdded = DateTime.Now;
                        await _testQuestionRepo.AddAsync(mapp);
                        res.Success = true;
                        res.Data = true;
                        res.Message = "Success";
                        return res;
                    }
                    
                }
                else
                {
                    res.Success = false;
                    res.Message = "Test or Question does not exist";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed :{ex.Message}";
                return res;
            }
        }*/
        public async Task<ResFormat<bool>> AddTestQuestion(TestQuestionAddDTO testQuestion)
        {
            var res = new ResFormat<bool>();
            try
            {
                var test = await _testRepo.GetByIdAsync(testQuestion.TestId);
                var question = await _questionRepo.GetByIdAsync(testQuestion.QuestionId);

                if (test == null || question == null)
                {
                    res.Success = false;
                    res.Message = "Test or Question does not exist";
                    return res;
                }

                var existingQuestions = (await _testQuestionRepo.GetQuestions())
                                        .Where(tq => tq.TestId == testQuestion.TestId).ToList();

                // Check if the test already has 8 questions
                if (existingQuestions.Count >= 8)
                {
                    res.Success = false;
                    res.Message = "Test already has the maximum number of 8 questions";
                    return res;
                }

                // Check if the same question already exists in the test
                if (existingQuestions.Any(tq => tq.QuestionId == testQuestion.QuestionId))
                {
                    res.Success = false;
                    res.Message = "Question already exists in Test";
                    return res;
                }

                // Check if a question of the same type already exists in the test
                /* var existingQuestionTypes = existingQuestions
                 *//*.Where(tq => tq.Question != null)*//*
                 .Select(tq => tq.Question.QtypeId)
                 .ToList();
                 var types = await _questionTypeRepo.GetAllAsync(); 

                     if (existingQuestionTypes.Contains((int)question.QtypeId)|| types.Any(c => c.QtypeId != question.QtypeId))
                     {
                         res.Success = false;
                         res.Message = "A question of this type already exists in the test";
                         return res;
                     }
                     else
                     {
                         var mapp = _mapper.Map<TestQuestion>(testQuestion);
                         mapp.DateAdded = DateTime.Now;
                         await _testQuestionRepo.AddAsync(mapp);
                     res.Success = true;
                     res.Data = true;
                     res.Message = "Success";
                     return res;
                 }*/
                var existingQuestionCounts = existingQuestions
                .GroupBy(tq => tq.Question.QtypeId)
                .ToDictionary(g => g.Key, g => g.Count());

                var types = await _questionTypeRepo.GetAllAsync();

                // Check if the question type already has 2 questions
                if (existingQuestionCounts.TryGetValue((int)question.QtypeId, out int count) && count >= 2)
                {
                    res.Success = false;
                    res.Message = "Each question type can only have 2 questions in the test";
                    return res;
                }
                else
                {
                    var mapp = _mapper.Map<TestQuestion>(testQuestion);
                    mapp.DateAdded = DateTime.Now;
                    await _testQuestionRepo.AddAsync(mapp);
                    res.Success = true;
                    res.Data = true;
                    res.Message = "Success";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"An error occurred: {ex.Message}";
                return res;
            }
        }



        public async Task<ResFormat<bool>> RemoveTestQuestion(TestQuestionAddDTO testQuestion)
        {
            var res = new ResFormat<bool>();
            try
            {
                var test = await _testRepo.GetByIdAsync(testQuestion.TestId);
                var question = await _questionRepo.GetByIdAsync(testQuestion.QuestionId);
                if (test != null || question != null)
                {
                    var list = await _testQuestionRepo.GetAllAsync();
                    if (list.Any(t => t.TestId == testQuestion.TestId && t.QuestionId == testQuestion.QuestionId))
                    {
                        var exist = list.FirstOrDefault(a => a.QuestionId == testQuestion.QuestionId && a.TestId == testQuestion.TestId);
                        await _testQuestionRepo.RemoveAsync(exist);
                        res.Success = true;
                        res.Data = true;
                        res.Message = "Success";
                        return res;
                    }
                    else
                    {
                        res.Success = false;
                        res.Message = "Test or Question in Test does not exist";
                        return res;
                    }

                }
                else
                {
                    res.Success = false;
                    res.Message = "Test or Question in Test does not exist";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Failed :{ex.Message}";
                return res;
            }
        }
    }
}
