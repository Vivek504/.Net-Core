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

        public bool IsClassCodeExist(int new_class_code)
        {
            var cls = context.Classes.Where(c => c.ClassCode == new_class_code).FirstOrDefault<Class>();
            if(cls != null && new_class_code == cls.ClassCode)
            {
                return true;
            }
            return false;
        }
    }
}
