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
    }
}
