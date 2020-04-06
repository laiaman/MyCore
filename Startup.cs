using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)//配置
        {
            services.AddDbContextPool<AppDbContext>(
                optionsAction: options => options.UseSqlServer(_configuration.GetConnectionString("StudentDBConnection"))
                );
            services.AddMvc().AddXmlSerializerFormatters();

            services.AddScoped<IStudentRepository, SQLStudentRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)//管理运行池的管道
        {
            if (env.IsDevelopment())
            {
                //DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();
                //developerExceptionPageOptions.SourceCodeLineCount = 5;
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseStaticFiles();
            //app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=home}/{action=index}/{id?}");
            });










            //app.Run(async (context) =>//终端型中间键
            //{
            //    //var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //    //   var config = _configuration["MyKey"];
            //    //  throw new Exception("管道异常");
            //    // context.Response.ContentType = "text/plain;charset=utf-8";
            //    await context.Response.WriteAsync("hello world ");

            //});



            //app.Use(async (context,next) =>
            //{
            //    //var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //    //   var config = _configuration["MyKey"];
            //    logger.LogInformation("1传入请求");
            //    context.Response.ContentType = "text/plain;charset=utf-8";
            //    await context.Response.WriteAsync("this is first Hello World!");
            //    await next();
            //    logger.LogInformation("1传出请求");
            //});

            //app.Use(async (context, next) =>
            //{
            //    //var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //    //   var config = _configuration["MyKey"];
            //    logger.LogInformation("2传入请求");
            //    await context.Response.WriteAsync("this is first Hello World!");
            //    await next();
            //    logger.LogInformation("2传出请求");
            //});





            //DefaultFilesOptions defaultFiles = new DefaultFilesOptions();
            //defaultFiles.DefaultFileNames.Clear();
            //defaultFiles.DefaultFileNames.Add("lonsid.html");


            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("lonsid.html");

            //   app.UseFileServer();


            //app.UseWelcomePage();
            //app.UseDefaultFiles();

            ////添加静态文件
            //app.UseStaticFiles();




        }
    }
}
