using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Classroom.Models
{
    public interface IClassRepository
    {
        Class Add(Class cls);
        bool IsClassCodeExist(int new_class_code);
        Teacher AddTeacher(Teacher teacher);
        Student AddStudent(Student student);
    }
}
