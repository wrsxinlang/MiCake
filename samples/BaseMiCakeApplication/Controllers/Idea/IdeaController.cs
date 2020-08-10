using BaseMiCakeApplication.Domain.Aggregates.Account;
using BaseMiCakeApplication.Domain.Aggregates.Idea;
using BaseMiCakeApplication.Domain.Repositories.NewIdeaBoundary;
using BaseMiCakeApplication.Models;
using MiCake.AspNetCore.Security;
using MiCake.DDD.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace BaseMiCakeApplication.Controllers.Idea
{
    [ApiController]
    [Route("[controller]/[action]")]
    [OpenApiTag("Idea 编辑创意", Description = "新增/编辑创意")]
    public class IdeaController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<NewIdea, Guid> _ideaRepositry;
        private readonly IRepository<User, Guid> _userRepositry;
        private readonly INewIdeaRepository _newIdeaRepository;
        public IdeaController(IHttpContextAccessor httpContextAccessor, IRepository<NewIdea, Guid> ideaRepositry, INewIdeaRepository newIdeaRepository, IRepository<User, Guid> userRepositry)
        {
            _httpContextAccessor = httpContextAccessor;
            _ideaRepositry = ideaRepositry;
            _newIdeaRepository = newIdeaRepository;
            _userRepositry = userRepositry;
        }

        /// <summary>
        /// 保存或编辑 创意
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ResultModel AddOrCreateIdea(AddIdeaDto form)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string userid = httpContext?.User.Claims.Where(s => s.Type == "userid").FirstOrDefault()?.Value;
            NewIdea newIdea = null;
            if (form.Id.HasValue)
            {
                newIdea = _ideaRepositry.Find(form.Id.Value);

                newIdea.Title = form.Title;
                newIdea.Introduce = form.Introduce;
                newIdea.Graphic = JsonConvert.SerializeObject(form.ContentList);
                newIdea.Remark = form.Remark;
                newIdea.ModificationTime = DateTime.Now;
                _ideaRepositry.Update(newIdea);
                newIdea.UpdateCommand(newIdea.Id);
            }
            else
            {
                newIdea = new NewIdea(form.Title, form.Introduce, JsonConvert.SerializeObject(form.ContentList), new Guid(userid), form.Remark);
                newIdea.RegisterCommand(newIdea.Id);
                _ideaRepositry.Add(newIdea);
            }

            return new ResultModel(0, newIdea);
        }

        /// <summary>
        /// 获取创意列表
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="orderType"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultModel GetIdeaDatas(string searchKey, string orderType, int page = 1, int limit = 9999)
        {
            var res = _newIdeaRepository.GetList(searchKey, page, limit, orderType, out int total);
            if (total == 0) return new ResultModel(0, 0, null);
            var list = new List<object>();
            foreach (var item in res)
            {
                var tempUser = _userRepositry.Find(item.CreateUserID);
                list.Add(new
                {
                    Oid = item.Id,
                    userAvtar = tempUser.Avatar,
                    userName = tempUser.Name,
                    cyintroduce = item.Introduce,
                    CommentCount = item.CommentCount,
                    ViewCount = item.ViewCount,
                    PublishCount = item.PublishCount,
                    Reputation = tempUser.Reputation
                });
            }
            return new ResultModel(0, total, list);
        }

        /// <summary>
        /// 获取创意详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultModel GetIdeaDetail(Guid id)
        {
            var newIdea =  _ideaRepositry.Find(id);
            if(newIdea==null) return new ResultModel(0, "数据不存在",null);
            var model = new {
                Id = newIdea.Id,
                Title = newIdea.Title,
                Introduce = newIdea.Introduce,
                ContentList = JsonConvert.DeserializeObject<List<IdeaItem>>(newIdea.Graphic),
                Remark = newIdea.Remark,
                useroid = newIdea.CreateUserID
            };
            return new ResultModel(0,model);
        }
    }

    public class AddIdeaDto
    {
        public AddIdeaDto()
        {
            ContentList = new List<IdeaItem>();
        }
        public Guid? Id { get; set; }

        //[Required(ErrorMessage = "The Title is Required")]
        //[MinLength(1)]
        //[MaxLength(50)]
        //[DisplayName("Title")]
        public string Title { get; set; }


        //[Required(ErrorMessage = "The Introduce is Required")]
        //[MinLength(2)]
        //[MaxLength(50)]
        //[DisplayName("Introduce")]
        public string Introduce { get; set; }

        public string Remark { get; set; }
        public List<IdeaItem> ContentList { get; set; }
    }

    public class IdeaItem
    {
        public int Type { get; set; }
        public string Value { get; set; }
        public string Url { get; set; }
    }
}
