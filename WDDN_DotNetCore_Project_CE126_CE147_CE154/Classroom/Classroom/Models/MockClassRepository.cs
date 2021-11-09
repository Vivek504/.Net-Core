using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Classroom.Models
{
    public class MockClassRepository : IClassRepository
    {
        private readonly ClassroomDbContext context;
        public MockClassRepository(ClassroomDbContext context)
        {
            this.context = context;
        }
        public Class Add(Class cls)
        {
            context.Classes.Add(cls);
            context.SaveChanges();
            return cls;
        }

        public Student AddStudent(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
            return student;
        }

        public Teacher AddTeacher(Teacher teacher)
        {
            context.Teachers.Add(teacher);
            context.SaveChanges();
            return teacher;
        }

        public Material AddMaterial(Material material)
        {
            context.Materials.Add(material);
            context.SaveChanges();
            return material;
        }

        public Class GetClass(int class_code)
        {
            return context.Classes.Where(c => c.ClassCode == class_code).FirstOrDefault<Class>();
        }

        public List<Student> GetStudent(string email)
        {
            return context.Students.Where(s => s.Email == email).ToList<Student>();
        }

        public List<Teacher> GetTeacher(string email)
        {
            return context.Teachers.Where(t => t.Email == email).ToList<Teacher>();
        }
        public List<Student> GetStudent(int class_code)
        {
            return context.Students.Where(s => s.ClassCode == class_code).ToList<Student>();
        }

        public List<Teacher> GetTeacher(int class_code)
        {
            return context.Teachers.Where(t => t.ClassCode == class_code).ToList<Teacher>();
        }

        public List<Material> GetMaterials(int class_code)
        {
            return context.Materials.Where(m => m.ClassCode == class_code).OrderByDescending(m => m.UploadTime).ToList<Material>();
        }

        public bool IsClassCodeExist(int new_class_code)
        {
            var cls = context.Classes.Where(c => c.ClassCode == new_class_code).FirstOrDefault<Class>();
            if(cls != null && new_class_code == cls.ClassCode)
            {
                return true;
            }
            return false;
        }
        public bool isTeacher(string email, int class_code)
        {
            var teacher = context.Teachers.Where(t => t.Email == email && t.ClassCode == class_code).FirstOrDefault<Teacher>();
            if (teacher != null)
                return true;
            return false;
        }
    }
}
