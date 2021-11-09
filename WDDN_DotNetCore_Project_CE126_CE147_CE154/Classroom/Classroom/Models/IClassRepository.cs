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
        Material AddMaterial(Material material);
        List<Teacher> GetTeacher(string email);
        List<Student> GetStudent(string email);
        List<Teacher> GetTeacher(int class_code);
        List<Student> GetStudent(int class_code);
        Class GetClass(int class_code);
        bool isTeacher(string email, int class_code);
        List<Material> GetMaterials(int class_code);
    }
}
