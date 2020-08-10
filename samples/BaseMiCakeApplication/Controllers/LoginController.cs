﻿using BaseMiCakeApplication.Domain.Aggregates;
using BaseMiCakeApplication.Domain.Repositories.UserBoundary;
using BaseMiCakeApplication.Dto;
using BaseMiCakeApplication.Dto.InputDto.Account;
using BaseMiCakeApplication.Models;
using BaseMiCakeApplication.WeChat;
using Mapster;
using MiCake.AspNetCore.Security;
using MiCake.DDD.Domain;
using MiCake.Identity.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiCakeApp = BaseMiCakeApplication.Domain.Aggregates.Account;

namespace BaseMiCakeApplication.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [OpenApiTag("Login 身份认证", Description = "登录/注册 以及获取相关信息")]
    public class LoginController : ControllerBase
    {
        private readonly IJwtSupporter _jwtSupporter;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepo;
        
        public LoginController(IJwtSupporter jwtSupporter, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _jwtSupporter = jwtSupporter;
            _httpContextAccessor = httpContextAccessor;
            _userRepo = userRepository;
        }

        [HttpPost]
        public async Task<ResultModel> Register(Dto.InputDto.Account.LoginUserInfo userDto)
        {
            var user = MiCakeApp.User.Create(userDto.Act, userDto.Pwd);
            await _userRepo.AddAsync(user);

            var token = _jwtSupporter.CreateToken(user);

            var userRes = new LoginResultDto() { AccessToken = token, HasUser = true, UserInfo = user.Adapt<UserDto>() };

            return new ResultModel(0, "", userRes);
        }

        [HttpPost]
        public async Task<ResultModel> GetToken(Dto.InputDto.Account.LoginUserInfo userDto)
        {

            var user = await _userRepo.LoginAction(userDto);

            if (user == null) return new ResultModel(-1, "账号不存在或密码错误",null);

            var token = _jwtSupporter.CreateToken(user);

            var userRes = new LoginResultDto() { AccessToken = token, HasUser = true, UserInfo = user.Adapt<UserDto>() };
           
            return new ResultModel(0, "", userRes);
        }

        [HttpGet]
        public ResultModel GetUserName(string name)
        {

            var user =  _userRepo.FindUserByName(name);

            if (user != null) return new ResultModel(-1, "用户已存在", null);
            return new ResultModel(0, "", "");
        }

        [HttpGet]
        [Authorize]
        public List<string> GetInfo([CurrentUser] Guid userID)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userInfo = httpContext?.User;

            var result = userInfo?.Claims.Select(s => s.Value).ToList();
            return result;
        }

        [HttpPost]
        [Authorize]
        public List<string> GetInfoWithDto([CurrentUser] [FromBody] GetUserInfoDto userID)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userInfo = httpContext?.User;

            var result = userInfo?.Claims.Select(s => s.Value).ToList();
            return result;
        }
    }

    public class GetUserInfoDto
    {
        [VerifyUserId]
        public Guid UserID { get; set; }

        public string UserName { get; set; }
    }
}
