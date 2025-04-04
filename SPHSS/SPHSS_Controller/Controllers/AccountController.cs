using BusinessObject.Enum;
using DataAccess.DTO.Req;
using DataAccess.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SPHSS_Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _accountService.GetAllAccount();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("Parents")]
        public async Task<IActionResult> GetAllParentAccounts()
        {
            var result = await _accountService.GetAllParentAccount();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("Psychologists")]
        public async Task<IActionResult> GetAllPsychologistAccounts()
        {
            var result = await _accountService.GetAllPsychologistAccount();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("Students")]
        public async Task<IActionResult> GetAllStudentAccounts()
        {
            var result = await _accountService.GetAllStudentAccount();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("StudentsByParent")]
        public async Task<IActionResult> GetAllStudentsAccountByParent()
        {
            var user = await _accountService.GetAcccountByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _accountService.GetAllStudentsAccountByParent(user.AccId);
            if (result == null || !result.Success)
            {
                return NotFound(result?.Message ?? "Failed to retrieve student accounts.");
            }
            return Ok(result);
        }
        [HttpGet("Unapproved")]
        public async Task<IActionResult> GetUnapproved()
        {
            var result = await _accountService.GetAllUnapprovedAccount();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(AccountRegisterDTO accountRegisterDTO)
        {
            var result = await _accountService.Register(accountRegisterDTO);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("RegisterParent")]
        public async Task<IActionResult> RegisterParent(AccountRegisterStudentByParentDTO accountRegisterDTO)
        {
            var result = await _accountService.RegisterParent(accountRegisterDTO);
            if (result == null || !result.Success)
            {
                return BadRequest(result?.Message ?? "Failed to register parent account.");
            }
            return Ok(result);
        }
        [HttpPost("RegisterStudentByParent")]
        public async Task<IActionResult> RegisterStudentByParent(AccountRegisterStudentByParentDTO accountRegisterDTO)
        {
            var user = await _accountService.GetAcccountByTokenAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _accountService.RegisterStudentByParent(accountRegisterDTO, user.AccId);
            if (result == null || !result.Success)
            {
                return BadRequest(result?.Message ?? "Failed to register student account.");
            }
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var account=await _accountService.Login(email, password);
            if (account.Success==false)
            {
                return Unauthorized("Invalid email or password.");
            }
            else
            {
                //Generate JWT Token
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true).Build();

                var claims = new List<Claim>
            {
                new Claim("Email", account.Data.AccEmail),
                new Claim("Role", account.Data.Role.ToString()),
                new Claim("UserId", account.Data.AccId.ToString()),
                new Claim("AccName", account.Data.AccName.ToString()),
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var preparedToken = new JwtSecurityToken(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                var token = new JwtSecurityTokenHandler().WriteToken(preparedToken);
                var roleId = account.Data.Role.ToString();
                var userId = account.Data.AccId.ToString();
                return Ok(new
                {
                    message = "Login successful",
                    token = token,
                    user = new
                    {
                        id = account.Data.AccId,
                        email = account.Data.AccEmail,
                        role = roleId,
                        name = account.Data.AccName,
                    }
                });
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Create(AccountCreateDTO accountCreateDTO)
        {
            var result = await _accountService.Create(accountCreateDTO);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _accountService.DeactivateAccount(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var result = await _accountService.GetAccountById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(AccountUpdateDTO account, int id)
        {
            var result = await _accountService.Update(account, id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPut("Approve")]
        public async Task<IActionResult> Approve(int id)
        {
            var result = await _accountService.ApproveAccount(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}
