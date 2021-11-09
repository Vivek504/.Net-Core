using Classroom.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Classroom.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClassRepository _classRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public Class[] classes { get; set; }

        public HomeController(IClassRepository classRepository,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _classRepository = classRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Route("Home")]
        public async Task<IActionResult> Index()
        {
            var current_user = await userManager.GetUserAsync(User);
            var teachers = _classRepository.GetTeacher(current_user.Email);
            var students = _classRepository.GetStudent(current_user.Email);
            int array_len = teachers.Count();
            array_len += students.Count();
            classes = new Class[array_len];
            int len = 0;
            if (teachers != null)
            {
                foreach (var t in teachers)
                {
                    var cls = _classRepository.GetClass(t.ClassCode);
                    classes[len] = new Class();
                    classes[len].ClassName = cls.ClassName;
                    classes[len].SubName = cls.SubName;
                    classes[len].ClassCode = cls.ClassCode;
                    len++;
                }
            }
            if (students != null)
            {
                foreach (var s in students)
                {
                    var cls = _classRepository.GetClass(s.ClassCode);
                    classes[len] = new Class();
                    classes[len].ClassName = cls.ClassName;
                    classes[len].SubName = cls.SubName;
                    classes[len].ClassCode = cls.ClassCode;
                    len++;
                }
            }

            return View(classes);
        }

        [Route("Create-Class")]
        [HttpGet]
        public ViewResult CreateClass()
        {
            return View();
        }
        [Route("Create-Class")]
        [HttpPost]
        public async Task<IActionResult> CreateClass(Class model)
        {
            if (ModelState.IsValid)
            {
                Random rnd = new Random();
                do
                {
                    int new_cls_code = rnd.Next(100000, 999999);
                    if (!_classRepository.IsClassCodeExist(new_cls_code))
                    {
                        model.ClassCode = new_cls_code;
                        _classRepository.Add(model);
                        if (signInManager.IsSignedIn(User))
                        {
                            var current_user = await userManager.GetUserAsync(User);
                            Teacher teacher = new Teacher();
                            teacher.ClassCode = new_cls_code;
                            teacher.Email = current_user.Email;
                            teacher.FName = current_user.FirstName;
                            teacher.LName = current_user.LastName;
                            _classRepository.AddTeacher(teacher);
                            return RedirectToAction("index", "home");
                        }
                        else
                        {
                            break;
                        }
                    }
                } while (true);
            }
            return View(model);
        }

        [Route("Join-Class")]
        [HttpGet]
        public ViewResult JoinClass()
        {
            return View();
        }
        [Route("Join-Class")]
        [HttpPost]
        public async Task<IActionResult> JoinClass(Class model)
        {
            if (model.ClassCode.ToString() != "" && _classRepository.IsClassCodeExist(model.ClassCode))
            {
                Student student = new Student();
                student.ClassCode = model.ClassCode;
                var current_user = await userManager.GetUserAsync(User);
                student.Email = current_user.Email;
                student.FName = current_user.FirstName;
                student.LName = current_user.LastName;
                _classRepository.AddStudent(student);
                return RedirectToAction("index", "home");
            }
            else if (model.ClassCode.ToString() != null)
            {
                ViewBag.Error = "Class does not exist.";
            }
            return View(model);
        }
    }
}
