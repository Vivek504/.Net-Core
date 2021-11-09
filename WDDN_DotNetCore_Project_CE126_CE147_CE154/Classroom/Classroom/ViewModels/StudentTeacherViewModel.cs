using Classroom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Classroom.ViewModels
{
    public class StudentTeacherViewModel
    {
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
