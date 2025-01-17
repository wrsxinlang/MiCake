﻿using BaseMiCakeApplication.Controllers.Comman;
using BaseMiCakeApplication.Utils;
using MiCake.AspNetCore.Identity;
using MiCake.Audit;
using MiCake.DDD.Domain;
using MiCake.Identity.Authentication;
using System;

namespace BaseMiCakeApplication.Domain.Aggregates.Account
{
    public class User : MiCakeUser<Guid>, IAggregateRoot<Guid>, IHasCreationTime, IHasModificationTime
    {
        [JwtClaim]
        public string Name { get; private set; }

        public string Avatar { get; private set; }

        public int Age { get; private set; }

        public Gender Gender { get; set; }

        public string Phone { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        /// <summary>
        /// 信誉值
        /// </summary>
        public int Reputation { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? ModificationTime { get; set; }

        //用于生成jwtToken
        [JwtClaim(ClaimName = GlobalArgs.ClaimUserId)]
        private Guid UserId => Id;

        public User()
        {
        }


        internal User(string name, string pwd,string avatar, Gender gender)
        {
            Id = Guid.NewGuid();
            Name = name;
            Password = pwd;
            Avatar = avatar;
            Gender = gender;
        }
        //internal User(string name, string phone, string pwd, int age)
        //{
        //    Id = Guid.NewGuid();
        //    Password = pwd;
        //    Phone = phone;
        //    Name = name;
        //    Age = age;
        //}

        public void SetAvatar(string avatar) => Avatar = avatar;

        public void ChangeUserInfo(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public void ChangePhone(string phone)
        {
            Phone = phone;
        }

        public static User Create(string name, string pwd, string avatar, Gender gender)
        {
            return new User(name, pwd, avatar, gender);
        }
    }
}
