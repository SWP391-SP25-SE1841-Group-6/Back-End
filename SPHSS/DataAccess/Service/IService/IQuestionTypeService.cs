using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.IService
{
    public interface IQuestionTypeService
    {
        Task<ResFormat<IEnumerable<ResQuestionTypeDTO>>> GetAllQuestionType();
        Task<ResFormat<ResQuestionTypeDTO>> GetQuestionTypeById(int id);
        Task<ResFormat<bool>> DeleteQuestionType(int id);
        Task<ResFormat<ResQuestionTypeDTO>> UpdateQuestionType(QuestionTypeCreateDTO questionType, int id);
        Task<ResFormat<ResQuestionTypeDTO>> CreateQuestionType(QuestionTypeCreateDTO questionType);
        Task<ResFormat<ResQuestionTypeDTO>> GetQuestionTypeByType(string type);
    }
}
