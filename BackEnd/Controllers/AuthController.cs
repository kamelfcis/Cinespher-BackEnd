using AuthenticationPlugin;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.ComponentModel.DataAnnotations;
using BackEnd.DTO.Auth;
using System.IO;
using BackEnd.Repository.Abstract;
using Microsoft.Win32;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IFileService _fileService;
        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        private CinespherContext _dbContext;
        public AuthController(CinespherContext dbContext, IConfiguration configuration, IFileService fileService)
        {
            _configuration = configuration;
            _auth = new AuthService(_configuration);
            _dbContext = dbContext;
            _fileService = fileService;
        }








        [HttpPost("CustomerRegister")]
        [AllowAnonymous]
        public async Task<IActionResult> CustomerRegister(RegisterDTO account)
        {
            var accountWithSameEmail = _dbContext.Users.SingleOrDefault(u => u.Email == account.Email || u.Username == account.Username);
            if (accountWithSameEmail != null) return BadRequest("User with this  UserName already exists");
            var accountObj = new User
            {
                Username = account.Username,
                Phone = account.Phone,
                Email = account.Email,
                Password = SecurePasswordHasherHelper.Hash(account.Password),
                FullName = account.FullName,
                Role = account.Role
            };
            _dbContext.Users.Add(accountObj);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPost("CustomerLogin")]
        [AllowAnonymous]
        public IActionResult Login(Login account)
        {
            var AccountUser = _dbContext.Users.FirstOrDefault(u => u.Username == account.Username);
            if (AccountUser == null) return StatusCode(StatusCodes.Status404NotFound);
            var hashedPassword = AccountUser.Password;
            if (!SecurePasswordHasherHelper.Verify(account.Password, hashedPassword)) return Unauthorized();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, AccountUser.Email ),
                new Claim(ClaimTypes.Name, AccountUser.Username),
            };

            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                token_type = token.TokenType,
                user_Id = AccountUser.UserId,
                user_name = AccountUser.Username,
                expires_in = token.ExpiresIn,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                accountid = AccountUser.UserId,
                email = AccountUser.Email,
                Role = AccountUser.Role,

            });
        }

        [HttpPost("CustomerAccountLogin")]
        [AllowAnonymous]
        public IActionResult CustomerAccountLogin(Login account)
        {
            var AccountUser = _dbContext.Users.FirstOrDefault(u => u.Username == account.Username);
            if (AccountUser == null) return StatusCode(StatusCodes.Status404NotFound);
            var hashedPassword = AccountUser.Password;
            if (!SecurePasswordHasherHelper.Verify(account.Password, hashedPassword)) return Unauthorized();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, AccountUser.Email ),
                new Claim(ClaimTypes.Name, AccountUser.Username),
            };

            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                token_type = token.TokenType,
                user_Id = AccountUser.UserId,
                user_name = AccountUser.Username,
                expires_in = token.ExpiresIn,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                accountid = AccountUser.UserId,
                email = AccountUser.Email,

            });
        }

    }
}
