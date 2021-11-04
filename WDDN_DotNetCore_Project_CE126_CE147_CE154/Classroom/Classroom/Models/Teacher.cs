using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Classroom.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public int ClassCode { get; set; }
        public string Email { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
}
