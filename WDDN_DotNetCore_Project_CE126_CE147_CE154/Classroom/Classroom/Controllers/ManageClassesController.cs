using Classroom.Models;
using Classroom.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Classroom.Controllers
{
    public class ManageClassesController : Controller
    {
        private readonly IClassRepository _classRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public Class[] classes { get; set; }
        public string ClassCode = "_class_code";
        public string[] teachers_list { get; set; }
        public string[] students_list { get; set; }
        public string MaterialId = "";
        public string AssignmentId = "";

        public ManageClassesController(IClassRepository classRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _classRepository = classRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Route("Stream")]
        public async Task<IActionResult> Stream(int? class_code)
        {
            if(class_code != null)
                HttpContext.Session.SetInt32(ClassCode, (int)class_code);
            else
                class_code = HttpContext.Session.GetInt32(ClassCode).Value;
            var current_user = await userManager.GetUserAsync(User);
            ViewBag.isTeacher = _classRepository.isTeacher(current_user.Email, (int)class_code);

            TeacherMaterialViewModel model = new TeacherMaterialViewModel();
            Teacher[] teacher = _classRepository.GetTeacher((int)class_code).ToArray();
            model.teacher_name = teacher[0].FName + " " + teacher[0].LName;
            model.Materials = _classRepository.GetMaterials((int)class_code);
            ViewBag.ClassCode = class_code;
            return View(model);
        }
        [Route("ViewMaterial")]
        [HttpGet]
        public async Task<ViewResult> ViewMaterial(int material_id)
        {
            HttpContext.Session.SetInt32(MaterialId,material_id);
            int class_code = HttpContext.Session.GetInt32(ClassCode).Value;
            var current_user = await userManager.GetUserAsync(User);
            ViewBag.isTeacher = _classRepository.isTeacher(current_user.Email, class_code);
            Material material = _classRepository.GetMaterial(material_id);
            return View(material);
        }
        
        [Route("ViewMaterial")]
        public async Task<IActionResult> SubmitAssignment(IFormFile file, Submission model)
        {
            int class_code = HttpContext.Session.GetInt32(ClassCode).Value;
            var current_user = await userManager.GetUserAsync(User);
            if (file != null)
            {
                Submission submission = new Submission();
                submission.ClassCode = class_code;
                submission.Email = current_user.Email;
                submission.SubmissionTime = DateTime.Now;
                var fileName = Path.GetFileName(file.FileName);
                var fileExtension = Path.GetExtension(fileName);
                submission.DocName = fileName;
                submission.DocType = fileExtension;
                using (var target = new MemoryStream())
                {
                    file.CopyTo(target);
                    submission.Document = target.ToArray();
                }
                _classRepository.AddSubmission(submission);
                
            }
            return RedirectToAction("Stream");
        }
        [Route("ViewFile")]
        public IActionResult ViewFile()
        {
            int material_id = HttpContext.Session.GetInt32(MaterialId).Value;
            Material material = _classRepository.GetMaterial(material_id);
            ViewData["pdf"] = material.Document;
            return View();
        }
        [Route("DownloadFile")]
        public FileResult DownloadFile() 
        {
            int material_id = HttpContext.Session.GetInt32(MaterialId).Value;
            Material material = _classRepository.GetMaterial(material_id);
            return File(material.Document,"application/pdf",material.DocName);
        }


        [Route("People")]
        public async Task<IActionResult> People()
        {
            int class_code = HttpContext.Session.GetInt32(ClassCode).Value;
            var current_user = await userManager.GetUserAsync(User);
            ViewBag.isTeacher = _classRepository.isTeacher(current_user.Email, class_code);
            StudentTeacherViewModel mymodel = new StudentTeacherViewModel();
            mymodel.Teachers = _classRepository.GetTeacher(class_code);
            mymodel.Students = _classRepository.GetStudent(class_code);
            return View(mymodel);
        }
        [Route("ViewSubmission")]
        public IActionResult ViewSubmission()
        {
            int class_code = HttpContext.Session.GetInt32(ClassCode).Value;
            List<Submission> submissions = _classRepository.GetSubmissions(class_code);
            StudentSubmissionViewModel model = new StudentSubmissionViewModel();
            model.Submissions = submissions;
            List<Student> SubmittedStudent = new List<Student>();
            List<string> email_list = new List<string>();
            foreach (var s in submissions)
            {
                Student student = _classRepository.GetStudent(s.Email)[0];
                SubmittedStudent.Add(student);
                if (!email_list.Contains(student.Email))
                {
                    email_list.Add(student.Email);
                }
            }
            model.SubmittedStudents = SubmittedStudent;
            ViewBag.assignedStudents = _classRepository.GetStudent(class_code).Count();
            ViewBag.turnedinStudents = email_list.Count();
            return View(model);
        }

        [Route("AddMaterial")]
        [HttpGet]
        public IActionResult AddMaterial()
        {
            return View();
        }
        [Route("AddMaterial")]
        [HttpPost]
        public IActionResult AddMaterial(IFormFile file, Material model)
        {
            Material material = new Material();
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var fileExtension = Path.GetExtension(fileName);
                material.DocName = fileName;
                material.DocType = fileExtension;
                using (var target = new MemoryStream())
                {
                    file.CopyTo(target);
                    material.Document = target.ToArray();
                }
            }
            material.ClassCode = HttpContext.Session.GetInt32(ClassCode).Value;
            material.Title = model.Title;
            material.Desc = model.Desc;
            material.UploadTime = DateTime.Now.Date;
            if (model.IsAssignment)
            {
                material.IsAssignment = true;
                if (model.Deadline != null)
                {
                    material.Deadline = model.Deadline;
                }
            }
            else
                material.IsAssignment = false;
            _classRepository.AddMaterial(material);
            return RedirectToAction("Stream");
        }
        [Route("ViewAssignment")]
        public ViewResult ViewAssignment(int id)
        {
            Submission submission = _classRepository.GetSubmission(id);
            var students = _classRepository.GetStudent(submission.Email);
            ViewBag.name = students[0].FName + " " + students[0].LName;
            HttpContext.Session.SetInt32(AssignmentId, id);
            return View(submission);
        }
        [Route("DownloadAssignment")]
        public FileResult DownloadAssignment()
        {
            int assignment_id = HttpContext.Session.GetInt32(AssignmentId).Value;
            Submission submission = _classRepository.GetSubmission(assignment_id);
            return File(submission.Document, "application/pdf", submission.DocName);
        }
        [Route("ViewAssignmentFile")]
        public IActionResult ViewAssignmentFile()
        {
            int assignment_id = HttpContext.Session.GetInt32(AssignmentId).Value;
            Submission submission = _classRepository.GetSubmission(assignment_id);
            ViewData["pdf"] = submission.Document;
            return View();
        }
    }
}
