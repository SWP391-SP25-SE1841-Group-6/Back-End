﻿using AutoMapper;
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
        private readonly IBaseRepo<TestQuestion> _testQuestionRepo;
        private readonly IMapper _mapper;
        private readonly IBaseRepo<Test> _testRepo;
        private readonly IBaseRepo<Question> _questionRepo;

        public TestQuestionService(IBaseRepo<TestQuestion> testQuestionRepo, IMapper mapper, IBaseRepo<Test> testRepo, IBaseRepo<Question> questionRepo)
        {
            _testQuestionRepo = testQuestionRepo;
            _mapper = mapper;
            _testRepo = testRepo;
            _questionRepo = questionRepo;

        }
        public async Task<ResFormat<bool>> AddTestQuestion(TestQuestionAddDTO testQuestion)
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
