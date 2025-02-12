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
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var account=await _accountService.Login(email, password);
            if (account == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            //Generate JWT Token
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();

            var claims = new List<Claim>
            {
                new Claim("Email", account.Data.AccEmail),
                new Claim("Role", account.Data.RoleId.ToString()),
                new Claim("UserId", account.Data.AccId.ToString()),
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
            var roleId = account.Data.RoleId.ToString();
            var userId = account.Data.AccId.ToString();
            return Ok(new
            {
                message = "Login successful",
                token = token,
                user = new
                {
                    id = account.Data.AccId,
                    email = account.Data.AccEmail,
                    role=roleId,
                }
            });
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
    }
}
