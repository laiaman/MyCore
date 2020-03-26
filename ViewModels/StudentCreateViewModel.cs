using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class StudentCreateViewModel
    {
        public int Id { get; set; }
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "请输入名字"), MaxLength(50, ErrorMessage = "名字太长")]
        public string Name { get; set; }
        [Display(Name = "年级信息")]
        [Required]
        public ClassNameEnum? ClassName { get; set; }
        [Display(Name = "邮箱")]
        [Required]
        public string Email { get; set; }
        [Display(Name ="图片")]
        public List<IFormFile> Photos { get; set; }

    }
}
