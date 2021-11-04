using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Classroom.Models
{
    public class ClassroomDbContext : IdentityDbContext<ApplicationUser>
    {
        public ClassroomDbContext(DbContextOptions<ClassroomDbContext> options) : base(options)
        {

        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
