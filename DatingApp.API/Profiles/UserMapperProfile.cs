using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Database.Entities;
using DatingApp.API.DTOs;
using DatingApp.API.Extensions;

namespace DatingApp.API.Profiles
{
    public class UserMapperProfile: Profile
    {
        public UserMapperProfile(){
            CreateMap<User, MemberDto>()
            // .ForMember(
            //     dest => dest.Age, 
            //     opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge())
            //     )
                ;
        }
    }
}