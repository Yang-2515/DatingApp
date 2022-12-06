using System.Collections.Generic;
using DatingApp.API.Database.Entities;
using DatingApp.API.DTOs;

namespace DatingApp.API.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserByUsername(string username);
        IEnumerable<MemberDto> GetMembers();
        MemberDto GetMemberByUsername(string username);
        User GetUserById(int Id);
        void CreateUser(User user);
        bool SaveChanges();
    }
}