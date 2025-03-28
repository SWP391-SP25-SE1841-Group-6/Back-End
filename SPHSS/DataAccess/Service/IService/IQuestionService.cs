﻿using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.IService
{
    public interface IQuestionService
    {
        Task<ResFormat<ResQuestionDTO>> Create(QuestionCreateDTO question);
        Task<ResFormat<IEnumerable<ResQuestionDTO>>> GetAllQuestions();
        Task<ResFormat<ResQuestionDTO>> GetQuestionById(int id);
        Task<ResFormat<bool>> Delete(int id);
        Task<ResFormat<ResQuestionDTO>> Update(QuestionCreateDTO question, int id);
    }
}
