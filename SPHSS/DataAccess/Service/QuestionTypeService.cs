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
    public class QuestionTypeService : IQuestionTypeService
    {
        private readonly IQuestionTypeRepo _questionTypeRepo;
        private readonly IMapper _mapper;

        public QuestionTypeService(IQuestionTypeRepo questionTypeRepo, IMapper mapper)
        {
            _questionTypeRepo = questionTypeRepo;
            _mapper = mapper;
        }

        public async Task<ResFormat<ResQuestionTypeDTO>> CreateQuestionType(QuestionTypeCreateDTO questionType)
        {
            var res = new ResFormat<ResQuestionTypeDTO>();
            try
            {
                var list = await _questionTypeRepo.GetAllAsync();
                if (list.Any(q => q.Qtype == questionType.Qtype))
                {
                    res.Success = false;
                    res.Message = "Duplicate value";
                    return res;
                }
                else
                {
                    var mapp = _mapper.Map<QuestionType>(questionType);
                    mapp.IsDeleted = false;
                    await _questionTypeRepo.AddAsync(mapp);
                    var result = _mapper.Map<ResQuestionTypeDTO>(mapp);
                    res.Success = true;
                    res.Data = result;
                    res.Message = "Question Type Created Successfully";
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

        public async Task<ResFormat<bool>> DeleteQuestionType(int id)
        {
            var res = new ResFormat<bool>();
            try
            {
                var list = await _questionTypeRepo.GetAllAsync();
                if (list.Any(q => q.QtypeId == id))
                {
                    var qtype = list.FirstOrDefault(q => q.QtypeId == id);
                    qtype.IsDeleted = true;
                    _questionTypeRepo.Update(qtype);
                    res.Success = true;
                    res.Data = true;
                    res.Message = "Success";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No questionType with this Id";
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

        public async Task<ResFormat<IEnumerable<ResQuestionTypeDTO>>> GetAllQuestionType()
        {
            var res = new ResFormat<IEnumerable<ResQuestionTypeDTO>>();
            try
            {
                var list = await _questionTypeRepo.GetAllAsync();
                var resList = _mapper.Map<IEnumerable<ResQuestionTypeDTO>>(list);
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

        public async Task<ResFormat<ResQuestionTypeDTO>> GetQuestionTypeById(int id)
        {
            var res = new ResFormat<ResQuestionTypeDTO>();
            try
            {

                var list = await _questionTypeRepo.GetAllAsync();
                if (list.Any(a => a.QtypeId == id && a.IsDeleted == false))
                {
                    var questionType = list.FirstOrDefault(a => a.QtypeId == id);
                    var resList = _mapper.Map<ResQuestionTypeDTO>(questionType);
                    res.Success = true;
                    res.Data = resList;
                    res.Message = "Retrieved successfully";
                    res.Message = "Success";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No questionType with this Id";
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

        public async Task<ResFormat<ResQuestionTypeDTO>> GetQuestionTypeByType(string type)
        {
            var res = new ResFormat<ResQuestionTypeDTO>();
            try
            {

                var list = await _questionTypeRepo.GetQuestionTypeAndQuestionsByType(type);
                if (list!=null)
                {
                    var resList = _mapper.Map<ResQuestionTypeDTO>(list);
                    res.Success = true;
                    res.Data = resList;
                    res.Message = "Retrieved successfully";
                    res.Message = "Success";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No questionType with this Id";
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

        public async Task<ResFormat<ResQuestionTypeDTO>> UpdateQuestionType(QuestionTypeCreateDTO questionType, int id)
        {
            var res = new ResFormat<ResQuestionTypeDTO>();
            try
            {

                var list = await _questionTypeRepo.GetAllAsync();
                if (list.Any(a => a.QtypeId == id && a.IsDeleted == false))
                {
                    var qtype = list.FirstOrDefault(a => a.QtypeId == id);
                    if (list.Any(b => b.Qtype == questionType.Qtype))
                    {

                        res.Success = false;
                        res.Message = "Duplicate Question Type";
                        return res;
                    }
                    else
                    {
                        qtype.Qtype = questionType.Qtype;
                        _questionTypeRepo.Update(qtype);
                        var qtype2 = _mapper.Map<ResQuestionTypeDTO>(qtype);
                        res.Success = true;
                        res.Data = qtype2;
                        res.Message = "Success";
                        return res;
                    }
                }
                else
                {
                    res.Success = false;
                    res.Message = "No questionType with this Id";
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
