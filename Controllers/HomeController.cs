using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly HostingEnvironment hostingEnvironment;
        private readonly ILogger logger;

        public HomeController(IStudentRepository studentRepository,HostingEnvironment hostingEnvironment,ILogger<HomeController> logger)
        {
            _studentRepository = studentRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Student> students = _studentRepository.GetAllStudents();
            
            return View(students);
         
        }

        public ViewResult Details(int id)
        {
            logger.LogInformation("info");
            logger.LogDebug("debug");
            Student student = _studentRepository.GetStudent(id);
            if (student == null)
            {
                Response.StatusCode = 404;
                return View("StudentNotFound", id);
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Student =student,
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


                Student newStu = new Student
                {
                    Name = stu.Name,
                    Email = stu.Email,
                    ClassName = stu.ClassName,
                    PhotoPath = ProcessUploadFile(stu)
                };
                _studentRepository.add(newStu);
            }
            return View();
        }

        private string ProcessUploadFile(StudentCreateViewModel stu)
        {
            string uniqueFileName = null;
            if ( stu.Photos.Count > 0)
            {
                foreach (var photo in stu.Photos)
                {

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream= new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                    
                }



            }

            return uniqueFileName;
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
                stu.Name = model.Name;
                stu.Email = model.Email;
                stu.ClassName = model.ClassName;
                if (model.Photos.Count>0)
                {
                    if (model.ExistingPhotoPath!=null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);

                    }
                    stu.PhotoPath = ProcessUploadFile(model);


                }
                Student updatestu = _studentRepository.Update(stu);
                return RedirectToAction("Index");

            }
            return View();
        }

    }
}