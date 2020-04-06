using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class ErrorController:Controller
    {
        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode) 
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                
                case 404:

                    ViewBag.ErrorMessage = "页面不存在";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QueryStr = statusCodeResult.OriginalQueryString;
                    ViewBag.BasePath = statusCodeResult.OriginalPathBase;
                    break;
        
            }
            return View("NotFound");
        }
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
           var exception= HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exception.Path;
            ViewBag.ExceptionMessage = exception.Error.Message;
            ViewBag.StackTrace = exception.Error.StackTrace;
            return View();
        }
    }
}
