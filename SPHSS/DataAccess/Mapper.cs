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

<<<<<<< HEAD
            #region Blog
            CreateMap<Blog, BlogCreateDTO>().ReverseMap();
            CreateMap<Blog, ResBlogCreateDTO>().ReverseMap();
            //<Blog, BlogUpdateDTO>().ReverseMap();
            #endregion
=======
            #region QuestionType
            CreateMap<QuestionType, QuestionTypeCreateDTO>().ReverseMap();
            CreateMap<QuestionType, ResQuestionTypeDTO>()
                .ForMember(x=>x.ListQuestions, o=>o.MapFrom(q=>q.Questions))
                .ReverseMap();
            #endregion

            #region Question
            CreateMap<Question, ResQuestionDTO>()
                .ForMember(x => x.Qtype, o => o.MapFrom(q => q.Qtype.Qtype))
                .ReverseMap();
            
            CreateMap<Question, QuestionCreateDTO>().ReverseMap();
            #endregion

<<<<<<< HEAD
>>>>>>> 06fb49e5a20bf27d5d4266642ccd64706e783f60
=======
            #region Test
            CreateMap<Test, ResTestDTO>().ReverseMap();
            CreateMap<Test, TestCreateDTO>().ReverseMap();
            CreateMap<Test, TestUpdateDTO>().ReverseMap();
            #endregion

>>>>>>> main
        }
    }
}
