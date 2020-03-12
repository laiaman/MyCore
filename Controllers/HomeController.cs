using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public HomeController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Student> students = _studentRepository.GetAllStudents();
            
            return View(students);
         
        }

        public ViewResult Details(int? id)
        {

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Student = _studentRepository.GetStudent(id??1),
                Title = "详细信息"
            };

           return View(homeDetailsViewModel);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }    
        [HttpPost]
        public IActionResult Create(Student stu)
        {
            if (ModelState.IsValid)
            {
                 _studentRepository.add(stu);
                return RedirectToAction("Details", new { id = stu.Id });
            }
            return View();
        }

    }
}