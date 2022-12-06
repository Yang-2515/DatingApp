using System.Collections.Generic;
using DatingApp.API.Database.Entities;
using DatingApp.API.Repositories;
using DatingApp.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace DatingApp.API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<MemberDto>> Get()
        {
            return Ok(_userRepository.GetMembers());
        }
        // [Authorize]
        // [HttpGet("{id}")]
        // public ActionResult<User> Get(int id)
        // {
        //     var user = _userRepository.GetUserById(id);
        //     if(user == null){
        //         return NotFound();
        //     }
        //     //var age = user.DateOfBirth.CalculateAge();
        //     else return Ok(user);
        // }

        [Authorize]
        [HttpGet("{username}")]
        public ActionResult<MemberDto> Get(string username)
        {
            var user = _userRepository.GetMemberByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            //var age = user.DateOfBirth.CalculateAge();
            else return Ok(user);
        }
    }
}