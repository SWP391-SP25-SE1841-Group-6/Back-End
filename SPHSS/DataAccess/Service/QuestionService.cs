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
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepo _questionRepo;
        private readonly IMapper _mapper;
        private readonly IQuestionTypeRepo _questionTypeRepo;

        public QuestionService(IQuestionRepo questionRepo, IMapper mapper,IQuestionTypeRepo questionTypeRepo)
        {
            _questionRepo = questionRepo;
            _mapper = mapper;
            _questionTypeRepo = questionTypeRepo;
        }
        public async Task<ResFormat<ResQuestionDTO>> Create(QuestionCreateDTO question)
        {
            var res = new ResFormat<ResQuestionDTO>();
            try
            {
                var list = await _questionRepo.GetAllAsync();
                var existingType = await _questionTypeRepo.GetByIdAsync(question.QtypeId);
                if (list.Any(q => q.Question1 == question.Question1))
                {
                    res.Success = false;
                    res.Message = "Duplicate value";
                    return res;
                }
                else if (existingType == null)
                {
                    res.Success = false;
                    res.Message = "Question Type do not exist";
                    return res;
                }
                else
                {
                    var mapp = _mapper.Map<Question>(question);
                    mapp.IsDeleted = false;
                    await _questionRepo.AddAsync(mapp);
                    var result = _mapper.Map<ResQuestionDTO>(mapp);
                    res.Success = true;
                    res.Data = result;
                    res.Message = "Question Created Successfully";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Create failed: {ex.Message}";
                return res;
            }
        }

        public async Task<ResFormat<bool>> Delete(int id)
        {
            var res = new ResFormat<bool>();
            try
            {
                var list = await _questionRepo.GetAllAsync();
                if (list.Any(q => q.QuestionId == id))
                {
                    var question = list.FirstOrDefault(q => q.QuestionId == id);
                    question.IsDeleted = true;
                    _questionRepo.Update(question);
                    res.Success = true;
                    res.Data = true;
                    res.Message = "Success";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No question with this Id";
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

        public Task<ResFormat<IEnumerable<ResQuestionDTO>>> GetAllQuestions()
        {
            throw new NotImplementedException();
        }

        public Task<ResFormat<ResQuestionDTO>> GetQuestionById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResFormat<ResQuestionDTO>> Update(QuestionCreateDTO question, int id)
        {
            var res = new ResFormat<ResQuestionDTO>();
            try
            {

                var list = await _questionRepo.GetAllAsync();
                if (list.Any(a => a.QuestionId == id && a.IsDeleted == false))
                {
                    var q = list.FirstOrDefault(a => a.QuestionId == id);
                    if (list.Any(b => b.Question1 == question.Question1))
                    {

                        res.Success = false;
                        res.Message = "Duplicate Question";
                        return res;
                    }
                    else
                    {
                        q.Question1 = question.Question1;
                        _questionRepo.Update(q);
                        var q2 = _mapper.Map<ResQuestionDTO>(q);
                        res.Success = true;
                        res.Data = q2;
                        res.Message = "Success";
                        return res;
                    }
                }
                else
                {
                    res.Success = false;
                    res.Message = "No question with this Id";
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
