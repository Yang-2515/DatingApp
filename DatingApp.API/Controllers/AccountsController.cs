using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Database;
using DatingApp.API.Database.Entities;
using DatingApp.API.DTOs;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountsController(DataContext context, ITokenService tokenService){
            _context = context;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public ActionResult<string> Register(RegisterDto register)
        {
            register.Username.ToLower();
            if(_context.Users.Any(u => u.Username == register.Username)){
                return BadRequest("Username is existed");
            }

            using var hmac = new HMACSHA512();
            var user = new User{
                Username = register.Username,
                Email = register.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(new UserResponse{
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            });
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginDto login)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == login.Username.ToLower());
            if(user == null){
                return Unauthorized("Invalid Username");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            for (var i = 0; i < computeHash.Length; i++){
                if(computeHash[i] != user.PasswordHash[i]){
                    return Unauthorized("Invalid Password");
                }
            }
            return Ok(new UserResponse{
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            });
        }
    }
}