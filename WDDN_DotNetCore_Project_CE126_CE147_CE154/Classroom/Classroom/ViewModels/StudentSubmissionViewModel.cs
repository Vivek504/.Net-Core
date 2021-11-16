using Classroom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Classroom.ViewModels
{
    public class StudentSubmissionViewModel
    {
        public IEnumerable<Student> SubmittedStudents { get; set; }
        public IEnumerable<Submission> Submissions { get; set; }
    }
}
