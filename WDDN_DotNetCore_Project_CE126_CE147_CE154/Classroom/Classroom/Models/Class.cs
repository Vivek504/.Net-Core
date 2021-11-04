using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Classroom.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        [Required(ErrorMessage = "Class code is required.")]
        public int ClassCode { get; set; }
        [Required(ErrorMessage = "Class name is required.")]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "Subject name is required.")]
        public string SubName { get; set; }
    }
}
