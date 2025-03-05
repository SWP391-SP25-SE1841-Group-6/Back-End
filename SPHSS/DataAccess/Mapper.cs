using AutoMapper;
using BusinessObject;
using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            #region Account
            CreateMap<Account, AccountRegisterDTO>().ReverseMap();
            CreateMap<Account, ResAccountLoginDTO>().ReverseMap();
            CreateMap<Account, AccountCreateDTO>().ReverseMap();
            CreateMap<Account, ResAccountCreateDTO>().ReverseMap();
            CreateMap<Account, AccountUpdateDTO>().ReverseMap();
            #endregion


            #region Blog
            CreateMap<Blog, BlogCreateDTO>().ReverseMap();
            CreateMap<Blog, ResBlogCreateDTO>().ReverseMap();
            //<Blog, BlogUpdateDTO>().ReverseMap();
            #endregion

            #region QuestionType
            CreateMap<QuestionType, QuestionTypeCreateDTO>().ReverseMap();
            CreateMap<QuestionType, ResQuestionTypeDTO>()
                .ForMember(x=>x.ListQuestions, o=>o.MapFrom(q=>q.Questions))
                .ReverseMap();
            #endregion

            #region TestQuestion
            CreateMap<TestQuestion, TestQuestionAddDTO>().ReverseMap();
            #endregion

            #region Question
            CreateMap<Question, ResQuestionDTO>()
                .ForMember(x => x.Qtype, o => o.MapFrom(q => q.Qtype.Qtype))
                .ReverseMap();
            
            CreateMap<Question, QuestionCreateDTO>().ReverseMap();
            #endregion

            #region Test
            CreateMap<Test, ResTestDTO>()
    .ForMember(dest => dest.ListQuestions,
               opt => opt.MapFrom(src => src.TestQuestions.Select(tq => tq.Question)))
    .ReverseMap();
            CreateMap<Test, TestCreateDTO>().ReverseMap();
            CreateMap<Test, TestUpdateDTO>().ReverseMap();
            #endregion

            #region TestResult
            CreateMap<TestResult, ResTestResultDTO>().ReverseMap();
            CreateMap<TestResult, TestResultCreateDTO>().ReverseMap();
            #endregion

            #region TestResultAnswer
            CreateMap<TestResultAnswer, ResTestResultAnswerDTO>().ReverseMap();
            CreateMap<TestResultAnswer, TestResultAnswerCreateDTO>().ReverseMap();
            #endregion

            #region TestResultDetail
            CreateMap<TestResultDetail, ResTestResultDetailDTO>().ReverseMap();
            CreateMap<TestResultDetail, TestResultDetailCreateDTO>().ReverseMap();
            #endregion

        }
    }
}
