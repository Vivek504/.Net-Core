using Classroom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Classroom.ViewModels
{
    public class TeacherMaterialViewModel
    {
        public IEnumerable<Material> Materials { get; set; }
        public string teacher_name { get; set; }
    }
}
