using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Classroom.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public int ClassCode { get; set; }
        public string Email { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
}
