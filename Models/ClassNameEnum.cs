using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public enum ClassNameEnum
    {
        [Display(Name ="无")]
        None,
        [Display(Name = "一年级")]
        First,
        [Display(Name = "二年级")]
        Second,
        [Display(Name = "三年级")]
        Third
    }
}
