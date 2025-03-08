using AutoMapper;
using Azure.Core;
using BusinessObject;
using BusinessObject.Enum;
using DataAccess.DTO.Req;
using DataAccess.DTO.Res;
using DataAccess.Repo;
using DataAccess.Repo.IRepo;
using DataAccess.Service.IService;
using DataAccess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class AccountService:IAccountService
    {
        private readonly IBaseRepo<Account> _accountRepo;
        private readonly IMapper _mapper;

        public AccountService(IBaseRepo<Account> accountRepo, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
        }

        public async Task<ResFormat<bool>> ApproveAccount(int id)
        {
            var res = new ResFormat<bool>();
            try
            {

                var list = await _accountRepo.GetAllAsync();
                if (list.Any(a => a.AccId == id && a.IsActivated == true && a.IsApproved == false))
                {
                    var login = list.FirstOrDefault(a => a.AccId == id);
                    login.IsApproved = true;
                    _accountRepo.Update(login);
                    res.Success = true;
                    res.Data = true;
                    res.Message = "Success";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No account with this Id or already Approved";
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

        public async Task<ResFormat<ResAccountCreateDTO>> Create(AccountCreateDTO account)
        {
            var res = new ResFormat<ResAccountCreateDTO>();
            try
            {
                var list = await _accountRepo.GetAllAsync();
                if (list.Any(a => a.AccName == account.AccName || a.AccEmail == account.AccEmail))
                {
                    res.Success = false;
                    res.Message = "Duplicate value";
                    return res;
                }
                else
                {
                    var mapp = _mapper.Map<Account>(account);
                    mapp.IsActivated = true;
                    mapp.IsApproved = true;
                    await _accountRepo.AddAsync(mapp);
                    var result = _mapper.Map<ResAccountCreateDTO>(mapp);
                    res.Success = true;
                    res.Data = result;
                    res.Message = "Created successfully";
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

        public async Task<ResFormat<bool>> DeactivateAccount(int id)
        {
            var res = new ResFormat<bool>();
            try
            {

                var list = await _accountRepo.GetAllAsync();
                if (list.Any(a => a.AccId == id&&a.IsActivated==true&&a.IsApproved==true))
                {
                    var login = list.FirstOrDefault(a => a.AccId == id);
                    login.IsActivated=false;
                    _accountRepo.Update(login);
                    res.Success = true;
                    res.Data = true;
                    res.Message = "Success";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No account with this Id";
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

        public async Task<Account> GetAcccountByTokenAsync(ClaimsPrincipal claims)
        {
            if (claims == null || claims.Identity.IsAuthenticated == false)
            {
                return null;
                throw new ArgumentNullException("Invalid token");

            }
            var userId = claims.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
            {
                throw new ArgumentException("No user can be found");
            }

            var user = await _accountRepo.GetByIdAsync(id);
            if (user == null)
            {
                throw new NullReferenceException("No user can be found");
            }
            return user;
        }

        public async Task<ResFormat<ResAccountCreateDTO>> GetAccountById(int id)
        {
            var res = new ResFormat<ResAccountCreateDTO>();
            try
            {

                var list = await _accountRepo.GetAllAsync();
                if (list.Any(a => a.AccId == id && a.IsActivated == true))
                {
                    var login = list.FirstOrDefault(a => a.AccId == id);
                    var resList=_mapper.Map<ResAccountCreateDTO>(login);
                    res.Success = true;
                    res.Data= resList;
                    res.Message = "Retrieved successfully";
                    res.Message = "Success";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No account with this Id";
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

        public async Task<ResFormat<IEnumerable<ResAccountCreateDTO>>> GetAllAccount()
        {
            var res = new ResFormat<IEnumerable<ResAccountCreateDTO>>();
            try
            {
                var list = await _accountRepo.GetAllAsync();
                var resList=_mapper.Map<IEnumerable<ResAccountCreateDTO>>(list);
                res.Success = true;
                res.Data= resList;
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

        public async Task<ResFormat<ResAccountLoginDTO>> Login(string email, string password)
        {
            var res= new ResFormat<ResAccountLoginDTO>();
            try
            {

                var list = await _accountRepo.GetAllAsync();
                var HashPass = HashPassWithSHA256.HashWithSHA256(password);
                if (list.Any(a => a.AccEmail == email && a.AccPass == HashPass))
                {
                    var login = list.FirstOrDefault(a => a.AccEmail == email && a.AccPass == HashPass);
                    var accLogin = _mapper.Map<ResAccountLoginDTO>(login);
                    res.Success = true;
                    res.Data = accLogin;
                    res.Message = "Success";
                    return res;
                }
                else
                {
                    res.Success = false;
                    res.Message = "Wrong email or password";
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

        public async Task<ResFormat<bool>> Register(AccountRegisterDTO account)
        {
            var res = new ResFormat<bool>();
            try
            {
                var list = await _accountRepo.GetAllAsync();
                if (list.Any(a => a.AccName == account.AccName || a.AccEmail == account.AccEmail))
                {
                    res.Success = false;
                    res.Data = false;
                    res.Message = "Duplicate value";
                    return res;
                }
                else
                {
                    var mapp = _mapper.Map<Account>(account);
                    mapp.AccPass = HashPassWithSHA256.HashWithSHA256(mapp.AccPass);
                    /*mapp.Role = Enum.RoleEnum;*/
                    mapp.IsActivated = true;
                    mapp.IsApproved = false;
                    await _accountRepo.AddAsync(mapp);
                    res.Success = true;
                    res.Data = true;
                    res.Message = "Register successfully";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"Register failed: {ex.Message}";
                return res;
            }
        }

        public async Task<ResFormat<ResAccountCreateDTO>> Update(AccountUpdateDTO accountCreateDTO, int id)
        {
            var res = new ResFormat<ResAccountCreateDTO>();
            try
            {
                var list = await _accountRepo.GetAllAsync();
                if (list.Any(a => a.AccId == id && a.IsActivated == true && a.IsApproved == true))
                {
                    var login = list.FirstOrDefault(a => a.AccId == id);
                    if (list.Any(b => b.AccEmail == accountCreateDTO.AccEmail)) {
                        
                        res.Success = false;
                        res.Message = "Duplicate Email";
                        return res;
                    }
                    else
                    {
                        login.AccName = accountCreateDTO.AccName;
                        login.AccPass = accountCreateDTO.AccPass;
                        login.AccEmail = accountCreateDTO.AccEmail;
                        login.Dob = accountCreateDTO.Dob;
                        login.Gender = accountCreateDTO.Gender;
                        _accountRepo.Update(login);
                        var accLogin = _mapper.Map<ResAccountCreateDTO>(login);
                        res.Success = true;
                        res.Data = accLogin;
                        res.Message = "Success";
                        return res;
                    }                       
                }
                else
                {
                    res.Success = false;
                    res.Message = "No account with this Id";
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
