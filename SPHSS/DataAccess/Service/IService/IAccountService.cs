﻿using BusinessObject;
using BusinessObject.Enum;
using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using DataAccess.Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service.IService
{
    public interface IAccountService
    {
        Task<ResFormat<IEnumerable<ResAccountCreateDTO>>> GetAllAccount();
        Task<ResFormat<IEnumerable<ResAccountCreateDTO>>> GetAllStudentAccount();
        Task<ResFormat<IEnumerable<ResAccountCreateDTO>>> GetAllPsychologistAccount();
        Task<ResFormat<IEnumerable<ResAccountCreateDTO>>> GetAllParentAccount();
        Task<ResFormat<IEnumerable<ResAccountCreateDTO>>> GetAllStudentsAccountByParent(int parentId);
        Task<ResFormat<IEnumerable<ResAccountCreateDTO>>> GetAllUnapprovedAccount();
        Task<ResFormat<ResAccountCreateDTO>> GetAccountById(int id);
        Task<ResFormat<ResAccountLoginDTO>> Login(string email, string password);
        Task<ResFormat<bool>> Register(AccountRegisterDTO account);
        Task<ResFormat<bool>> RegisterParent(AccountRegisterStudentByParentDTO accountRegisterDTO);
        Task<ResFormat<bool>> RegisterStudentByParent(AccountRegisterStudentByParentDTO accountRegisterDTO, int parentId);
        Task<ResFormat<bool>> DeactivateAccount(int id);
        Task<ResFormat<ResAccountCreateDTO>> Update(AccountUpdateDTO account, int id);
        Task<ResFormat<ResAccountCreateDTO>> Create(AccountCreateDTO account);
        Task<ResFormat<bool>> ApproveAccount(int id);
        Task<Account> GetAcccountByTokenAsync(ClaimsPrincipal claims);


    }
}
