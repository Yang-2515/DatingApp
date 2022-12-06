using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.API.Database;
using DatingApp.API.Database.Entities;
using DatingApp.API.DTOs;

namespace DatingApp.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _Context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
            _Context = dataContext;
        }

        public void CreateUser(User user)
        {
            if (GetUserByUsername(user.Username) != null)
            {
                return;
            }
            _Context.Users.Add(user);
        }

        public MemberDto GetMemberByUsername(string username)
        {
            return _Context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).FirstOrDefault(u => u.Username == username);
        }

        public IEnumerable<MemberDto> GetMembers()
        {
            return _Context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider);
        }

        public User GetUserById(int Id)
        {
            var user = _Context.Users.FirstOrDefault(u => u.Id == Id);
            return user;
        }

        public User GetUserByUsername(string username)
        {
            var user = _Context.Users.FirstOrDefault(u => u.Username == username);
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return _Context.Users;
        }

        public bool SaveChanges()
        {
            return (_Context.SaveChanges() > 0);
        }
    }
}