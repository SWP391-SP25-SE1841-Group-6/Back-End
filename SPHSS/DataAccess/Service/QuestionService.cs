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
                if (list.Any(q => q.Question1 == question.Question1 && q.IsDeleted == false))
                {
                    res.Success = false;
                    res.Message = "Duplicate value";
                    return res;
                }
                else if (existingType == null || existingType.IsDeleted == true)
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
                var existingQuestion = await _questionRepo.GetQuestionByIdWithType(id);
                if (existingQuestion != null)
                {
                    /*var question = list.FirstOrDefault(q => q.QuestionId == id);*/
                    existingQuestion.IsDeleted = true;
                    _questionRepo.Update(existingQuestion);
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

        public async Task<ResFormat<IEnumerable<ResQuestionDTO>>> GetAllQuestions()
        {
            var res = new ResFormat<IEnumerable<ResQuestionDTO>>();
            try
            {
                var list = await _questionRepo.GetAllQuestionsWithType();
                var resList = _mapper.Map<IEnumerable<ResQuestionDTO>>(list);
                res.Success = true;
                res.Data = resList;
                res.Message = "Retrieved successfully";
                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Retrieved failed: {ex.Message}";
                return res;
            }
        }

        public async Task<ResFormat<ResQuestionDTO>> GetQuestionById(int id)
        {
            var res = new ResFormat<ResQuestionDTO>();
            try
            {

                var existingQuestion = await _questionRepo.GetQuestionByIdWithType(id);
                if (existingQuestion != null)
                {
                    /*var question = list.FirstOrDefault(a => a.QuestionId == id);*/
                    var resList = _mapper.Map<ResQuestionDTO>(existingQuestion);
                    res.Success = true;
                    res.Data = resList;
                    res.Message = "Retrieved successfully";
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

        public async Task<ResFormat<ResQuestionDTO>> Update(QuestionCreateDTO question, int id)
        {
            var res = new ResFormat<ResQuestionDTO>();
            try
            {

                var existingQuestion = await _questionRepo.GetQuestionByIdWithType(id);
                if (existingQuestion != null)
                {
                    
                    if (existingQuestion.Question1 == question.Question1)
                    {

                        res.Success = false;
                        res.Message = "Duplicate Question";
                        return res;
                    }
                    else
                    {
                        existingQuestion.Question1 = question.Question1;
                        _questionRepo.Update(existingQuestion);
                        var q2 = _mapper.Map<ResQuestionDTO>(existingQuestion);
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
