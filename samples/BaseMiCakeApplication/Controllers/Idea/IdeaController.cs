using BaseMiCakeApplication.Domain.Aggregates.Idea;
using BaseMiCakeApplication.Models;
using MiCake.AspNetCore.Security;
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

        public IdeaController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Authorize]
        public ResultModel AddOrCreateIdea(AddIdeaDto form)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string userid = httpContext?.User.Claims.Where(s => s.Type == "userid").FirstOrDefault()?.Value;
            var newIdea = new NewIdea(form.Title, form.Introduce, JsonConvert.SerializeObject(form.ContentList), new Guid(userid));
            newIdea.RegisterCommand(newIdea.Id);
            return new ResultModel(0,newIdea);
        }
    }

    public class AddIdeaDto
    {
        public AddIdeaDto()
        {
            ContentList = new List<IdeaItem>();
        }
        public Guid Id { get; set; }

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

        [Required(ErrorMessage = "The ContentList is Required")]
        [DisplayName("ContentList")]
        public List<IdeaItem> ContentList { get; set; }
    }

    public class IdeaItem
    {
        public int Type { get; set; }
        public string Value { get; set; }
        public string Url { get; set; }
    }
}
