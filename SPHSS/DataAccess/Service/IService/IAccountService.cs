using BusinessObject;
using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using DataAccess.Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.IService
{
    public interface IAccountService
    {
        Task<ResFormat<IEnumerable<ResAccountCreateDTO>>> GetAllAccount();
        Task<ResFormat<ResAccountCreateDTO>> GetAccountById(int id);
        Task<ResFormat<ResAccountLoginDTO>> Login(string email, string password);
        Task<ResFormat<bool>> Register(AccountRegisterDTO account);
        Task<ResFormat<bool>> DeactivateAccount(int id);
        Task<ResFormat<ResAccountCreateDTO>> Update(AccountUpdateDTO account, int id);
        Task<ResFormat<ResAccountCreateDTO>> Create(AccountCreateDTO account);


    }
}
