using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public static class ModelBuilderExtesions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
    new Student
    {
        Id = 1,
        Name = "amon",
        ClassName = ClassNameEnum.Third,
        Email = "chen@lonsid.cn"
    },
    new Student
    {
        Id = 2,
        Name = "chenjianlin",
        ClassName = ClassNameEnum.First,
        Email = "cyj@lonsid.cn"
    }
    );
        }
    }
}
