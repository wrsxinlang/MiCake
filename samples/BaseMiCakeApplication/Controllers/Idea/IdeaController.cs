using BaseMiCakeApplication.Domain.Aggregates.Idea;
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

namespace BaseMiCakeApplication.Controllers.Idea
{
    [ApiController]
    [Route("[controller]/[action]")]
    [OpenApiTag("Idea 编辑创意", Description = "新增/编辑创意")]
    public class IdeaController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<NewIdea, Guid> _ideaRepositry;
        public IdeaController(IHttpContextAccessor httpContextAccessor, IRepository<NewIdea, Guid> ideaRepositry)
        {
            _httpContextAccessor = httpContextAccessor;
            _ideaRepositry = ideaRepositry;
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
                newIdea.UpdateCommand(newIdea.Id);
                _ideaRepositry.Update(newIdea);
            }
            else 
            {
                newIdea = new NewIdea(form.Title, form.Introduce, JsonConvert.SerializeObject(form.ContentList), new Guid(userid));
                newIdea.RegisterCommand(newIdea.Id);
                _ideaRepositry.Add(newIdea);
            }
            
            return new ResultModel(0,newIdea);
        }
    }

    public class AddIdeaDto
    {
        public AddIdeaDto()
        {
            ContentList = new List<IdeaItem>();
        }
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "The Title is Required")]
        [MinLength(1)]
        [MaxLength(50)]
        [DisplayName("Title")]
        public string Title { get; set; }


        [Required(ErrorMessage = "The Introduce is Required")]
        [MinLength(2)]
        [MaxLength(50)]
        [DisplayName("Introduce")]
        public string Introduce { get; set; }

     
        public List<IdeaItem> ContentList { get; set; }
    }

    public class IdeaItem
    {
        public int Type { get; set; }
        public string Value { get; set; }
        public string Url { get; set; }
    }
}
