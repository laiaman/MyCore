using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public interface IStudentRepository
    {/// <summary>
    /// 通过id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
        Student GetStudent(int id);
        IEnumerable<Student> GetAllStudents();
        Student add(Student student);

        Student Update(Student updateStudent);
        Student Delete(int id);
    }
}
