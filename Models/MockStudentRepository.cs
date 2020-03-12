﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class MockStudentRepository : IStudentRepository
    {
        private List<Student> _studentList;
        public MockStudentRepository()
        {
            _studentList = new List<Student>()
            {
            new Student() { Id = 1, Name = "张三", ClassName =ClassNameEnum.First, Email = "Tony-zhang@52abp.com" },
            new Student() { Id = 2, Name = "李四", ClassName = ClassNameEnum.Second, Email = "lisi@52abp.com" },
            new Student() { Id = 3, Name = "王二麻子", ClassName =ClassNameEnum.Third, Email = "wang@52abp.com" },
            };
        }

        public Student add(Student student)
        {
            student.Id = _studentList.Max(s => s.Id) + 1;
            _studentList.Add(student);
            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentList;
        }

        public Student GetStudent(int id)
        {
            return _studentList.FirstOrDefault(a => a.Id == id);
        }
        
    }
}
