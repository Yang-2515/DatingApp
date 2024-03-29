using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DatingApp.API.Database.Entities;
using System;
using System.Text;
using System.Security.Cryptography;

namespace DatingApp.API.Database
{
    public class Seed
    {
        public static void SeedUsers(DataContext context){
            if(context.Users.Any()) return;
            var userData = System.IO.File.ReadAllText("Database/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<User>>(userData);
            if(users == null) return;
            foreach(var user in users)
            {
                using var hmac = new HMACSHA512();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;
                user.CreatedAt = DateTime.Now;
                user.Username = user.Username.ToLower();
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}