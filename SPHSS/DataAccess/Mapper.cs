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

        }
    }
}
