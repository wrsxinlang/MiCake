using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseMiCakeApplication.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BaseMiCakeApplication.Controllers
{
   
    [EnableCors("AllowAny")] //启用Cors跨域,这里可以建一个Api控制器的基类,不必每一个都写
    public class OriginController : ControllerBase
    {
        [NonAction]
        protected virtual IActionResult ErrorResult(int code, string msg, object data = null)
        {
            var resObj = new ResultModel(code, msg, data);

            return Ok(resObj);
        }

        [NonAction]
        protected virtual IActionResult Result(int code, int count, object data = null)
        {
            var resObj = new ResultModel(code, count, data);
            return Ok(resObj);
        }

        [NonAction]
        protected virtual IActionResult Result(int count, object data = null)
        {
            var resObj = new ResultModel(count, data);
            return Ok(resObj);
        }

        [NonAction]
        protected virtual IActionResult Result(object data)
        {
            var resObj = new ResultModel(data);
            return Ok(resObj);
        }

        [NonAction]
        protected virtual IActionResult Result()
        {
            var resObj = new ResultModel();
            return Ok(resObj);
        }
    }
} 