using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly HostingEnvironment hostingEnvironment;

        public HomeController(IStudentRepository studentRepository,HostingEnvironment hostingEnvironment)
        {
            _studentRepository = studentRepository;
            this.hostingEnvironment = hostingEnvironment;

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
        public IActionResult Create(StudentCreateViewModel stu)
        {
            if (ModelState.IsValid)
            {
                //        _studentRepository.add(stu);
                //  return RedirectToAction("Details", new { id = stu.Id });
                string uniqueFileName = null;
                if (stu.Photos!=null&&stu.Photos.Count>0)
                {
                    foreach (var photo in stu.Photos)
                    {

                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }



                }
                Student newStu = new Student
                {
                    Name = stu.Name,
                    Email = stu.Email,
                    ClassName = stu.ClassName,
                    PhotoPath = uniqueFileName
                };
                _studentRepository.add(newStu);
            }
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {

            Student student = _studentRepository.GetStudent(id);
            if (student!=null)
            {
                StudentEditViewModel studentEditViewModel = new StudentEditViewModel
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    ClassName = student.ClassName,
                    ExistingPhotoPath = student.PhotoPath
                };
                return View(studentEditViewModel);
            }
            throw new Exception("null id");
        }
        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student stu = _studentRepository.GetStudent(model.Id);

                if (model.Photos.Count>0)
                {
                    if (model.ExistingPhotoPath!=null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);

                    }
                    string uniqueFileName = null;
                    if (model.Photos != null && model.Photos.Count > 0)
                    {
                        foreach (var photo in model.Photos)
                        {

                            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                            uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                            photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        }

                        stu.PhotoPath = uniqueFileName;

                    }


                }
                Student updatestu = _studentRepository.Update(stu);
                return RedirectToAction("Index");

            }
            return View();
        }

    }
}