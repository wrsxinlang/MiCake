using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BaseMiCakeApplication.Domain.Aggregates;
using BaseMiCakeApplication.Models;
using MiCake.DDD.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

namespace BaseMiCakeApplication.Controllers.Base
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UploadController : ControllerBase
    {
        public static string[] LimitPictureType = { ".PNG", ".JPG", ".JPEG", ".BMP", ".ICO" };
        private readonly string _targetFilePath = $"/UploadFile/{DateTime.Now:yyyyMMdd}/";
        private readonly IRepository<FileObject, Guid> _uploadFileReposity;

        private static IHostEnvironment env;

        public UploadController(IHostEnvironment _env, IRepository<FileObject, Guid> uploadFileReposity)
        {
            env = _env;
            _uploadFileReposity = uploadFileReposity;
        }

        [HttpPost]
        public ResultModel SaveFile(IFormFile file)
        {
            var fileSize = file.Length;
            if (fileSize > 0)
            {
                var fileEntity = new FileObject
                {
                    Id = Guid.NewGuid(),
                    FileExtention = Path.GetExtension(file.FileName).ToLower(),
                    Name = file.FileName,
                    FileLength = fileSize
                };
                var dNow = DateTime.Now;
                fileEntity.Url = Path.Combine("Upload", dNow.ToString("yyyy"), dNow.ToString("MM"), dNow.ToString("dd"));
                var path = Path.Combine(env.ContentRootPath, fileEntity.Url);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                fileEntity.Url = "\\" + fileEntity.Url;
                var savePath = Path.Combine(path, fileEntity.Id + fileEntity.FileExtention);
                using (var fs = new FileStream(savePath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
                _uploadFileReposity.AddAndReturn(fileEntity);

                var resFile = new
                {
                    Id = fileEntity.Id,
                    Path = Path.Combine(fileEntity.Url, fileEntity.Id.ToString() + fileEntity.FileExtention)
                };
                return new ResultModel(0, resFile);
            }
            return new ResultModel(-1, "文件读取失败", null);
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="imgName"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(string imgName)
        {
            var image = this.LoadingPhoto("\\Images\\6\\", imgName);
            if (image == null)
            {
                
            }

            return image;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        //[Authorize]
        public ResultModel UpLoadPhoto(IFormFile formFile)
        {
            ResultModel code;
            var currentPictureWithoutExtension = Path.GetFileNameWithoutExtension(formFile.FileName);
            var currentPictureExtension = Path.GetExtension(formFile.FileName).ToUpper();
            var path = Directory.GetCurrentDirectory() + _targetFilePath;
            if (LimitPictureType.Contains(currentPictureExtension))
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string name = currentPictureWithoutExtension + currentPictureExtension;
                path += name;
                using (var fs = System.IO.File.Create(path))
                {
                    formFile.CopyTo(fs);
                    //Stream 都有 Flush() 方法，
                    //根据官方文档的说法
                    //“使用此方法将所有信息从基础缓冲区移动到其目标或清除缓冲区，或者同时执行这两种操作”
                    fs.Flush();
                }
                code = new ResultModel();
                return code;
            }
            code = new ResultModel(-1, "上传失败", null);
            return code;
        }


        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public FileContentResult LoadingPhoto(string path, string name)
        {
            path = Directory.GetCurrentDirectory() + path + name + ".jpeg";
            FileInfo fi = new FileInfo(path);
            if (!fi.Exists)
            {
                return null;
            }

            FileStream fs = fi.OpenRead();
            byte[] buffer = new byte[fi.Length];
            //读取图片字节流
            //从流中读取一个字节块，并在给定的缓冲区中写入数据。
            fs.Read(buffer, 0, Convert.ToInt32(fi.Length));
            var resource = File(buffer, "image/jpeg");
            fs.Close();
            return resource;
        }


        public JsonResult SingleFileUpload()
        {
            var formFile = Request.Form.Files[0];//获取请求发送过来的文件
            var currentDate = DateTime.Now;
            var webRootPath = env.ContentRootPath;//>>>相当于HttpContext.Current.Server.MapPath("") 

            try
            {
                var filePath = $"/UploadFile/{currentDate:yyyyMMdd}/";

                //创建每日存储文件夹
                if (!Directory.Exists(webRootPath + filePath))
                {
                    Directory.CreateDirectory(webRootPath + filePath);
                }

                if (formFile != null)
                {
                    //文件后缀
                    var fileExtension = Path.GetExtension(formFile.FileName);//获取文件格式，拓展名

                    //判断文件大小
                    var fileSize = formFile.Length;

                    if (fileSize > 1024 * 1024 * 10) //10M TODO:(1mb=1024X1024b)
                    {
                        return new JsonResult(new { isSuccess = false, resultMsg = "上传的文件不能大于10M" });
                    }

                    //保存的文件名称(以名称和保存时间命名)
                    var saveName = formFile.FileName.Substring(0, formFile.FileName.LastIndexOf('.')) + "_" + currentDate.ToString("HHmmss") + fileExtension;

                    //文件保存
                    using (var fs = System.IO.File.Create(webRootPath + filePath + saveName))
                    {
                        formFile.CopyTo(fs);
                        fs.Flush();
                    }

                    //完整的文件路径
                    var completeFilePath = Path.Combine(filePath, saveName);

                    return new JsonResult(new { isSuccess = true, returnMsg = "上传成功", completeFilePath = completeFilePath });
                }
                else
                {
                    return new JsonResult(new { isSuccess = false, resultMsg = "上传失败，未检测上传的文件信息~" });
                }

            }
            catch (Exception ex)
            {
                return new JsonResult(new { isSuccess = false, resultMsg = "文件保存失败，异常信息为：" + ex.Message });
            }
        }

        /// <summary>
        /// Form表单之单文件上传
        /// </summary>
        /// <param name="formFile">form表单文件流信息</param>
        /// <returns></returns>
        public JsonResult FormSingleFileUpload(IFormFile formFile)
        {
            var currentDate = DateTime.Now;
            var webRootPath = env.ContentRootPath;//>>>相当于HttpContext.Current.Server.MapPath("") 

            try
            {
                var filePath = $"/UploadFile/{currentDate:yyyyMMdd}/";

                //创建每日存储文件夹
                if (!Directory.Exists(webRootPath + filePath))
                {
                    Directory.CreateDirectory(webRootPath + filePath);
                }

                if (formFile != null)
                {
                    //文件后缀
                    var fileExtension = Path.GetExtension(formFile.FileName);//获取文件格式，拓展名

                    //判断文件大小
                    var fileSize = formFile.Length;

                    if (fileSize > 1024 * 1024 * 10) //10M TODO:(1mb=1024X1024b)
                    {
                        return new JsonResult(new { isSuccess = false, resultMsg = "上传的文件不能大于10M" });
                    }

                    //保存的文件名称(以名称和保存时间命名)
                    var saveName = formFile.FileName.Substring(0, formFile.FileName.LastIndexOf('.')) + "_" + currentDate.ToString("HHmmss") + fileExtension;

                    //文件保存
                    using (var fs = System.IO.File.Create(webRootPath + filePath + saveName))
                    {
                        formFile.CopyTo(fs);
                        fs.Flush();
                    }

                    //完整的文件路径
                    var completeFilePath = Path.Combine(filePath, saveName);

                    return new JsonResult(new { isSuccess = true, returnMsg = "上传成功", completeFilePath = completeFilePath });
                }
                else
                {
                    return new JsonResult(new { isSuccess = false, resultMsg = "上传失败，未检测上传的文件信息~" });
                }

            }
            catch (Exception ex)
            {
                return new JsonResult(new { isSuccess = false, resultMsg = "文件保存失败，异常信息为：" + ex.Message });
            }

        }


        /// <summary>
        /// 流式文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost("UploadingStream")]
        public async Task<IActionResult> UploadingStream()
        {

            //获取boundary
            var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;
            //得到reader
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            //{ BodyLengthLimit = 2000 };//
            var section = await reader.ReadNextSectionAsync();

            //读取section
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);
                if (hasContentDispositionHeader)
                {
                    var trustedFileNameForFileStorage = Path.GetRandomFileName();
                    await WriteFileAsync(section.Body, Path.Combine(_targetFilePath, trustedFileNameForFileStorage));
                }
                section = await reader.ReadNextSectionAsync();
            }
            return Created(nameof(UploadController), null);
        }

        /// <summary>
        /// 缓存式文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("UploadingFormFile")]
        public async Task<IActionResult> UploadingFormFile(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var trustedFileNameForFileStorage = Path.GetRandomFileName();
                await WriteFileAsync(stream, Path.Combine(_targetFilePath, trustedFileNameForFileStorage));
            }
            return Created(nameof(UploadController), null);
        }


        /// <summary>
        /// 写文件导到磁盘
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="path">文件保存路径</param>
        /// <returns></returns>
        public static async Task<int> WriteFileAsync(System.IO.Stream stream, string path)
        {
            const int FILE_WRITE_SIZE = 84975;//写出缓冲区大小
            int writeCount = 0;
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write, FILE_WRITE_SIZE, true))
            {
                byte[] byteArr = new byte[FILE_WRITE_SIZE];
                int readCount = 0;
                while ((readCount = await stream.ReadAsync(byteArr, 0, byteArr.Length)) > 0)
                {
                    await fileStream.WriteAsync(byteArr, 0, readCount);
                    writeCount += readCount;
                }
            }
            return writeCount;
        }
    }
}