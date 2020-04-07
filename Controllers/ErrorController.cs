using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class ErrorController:Controller
    {
        private readonly ILogger<ErrorController> logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }


        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode) 
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                
                case 404:

                    ViewBag.ErrorMessage = "页面不存在";
                    logger.LogWarning($"发生了404错误,路径={statusCodeResult.OriginalPath}以及查询字符串{statusCodeResult.OriginalQueryString}");
                    //ViewBag.Path = statusCodeResult.OriginalPath;
                    //ViewBag.QueryStr = statusCodeResult.OriginalQueryString;
                    //ViewBag.BasePath = statusCodeResult.OriginalPathBase;
                    break;
        
            }
            return View("NotFound");
        }
        //[AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
           var exception= HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //ViewBag.ExceptionPath = exception.Path;
            //ViewBag.ExceptionMessage = exception.Error.Message;
            //ViewBag.StackTrace = exception.Error.StackTrace;
            logger.LogError($"路径{exception.Path},产生了一个错误{exception.Error.Message}");
            return View();
        }
    }
}
