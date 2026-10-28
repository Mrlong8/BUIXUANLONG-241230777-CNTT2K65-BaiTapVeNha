using Microsoft.EntityFrameworkCore;
using Lap3AndLap4.Models;

namespace Lap3AndLap4.Models
{
    public class NewDbContext : DbContext
    {
        public NewDbContext(DbContextOptions<NewDbContext> options) : base(options)
        {
        }

        // Khai báo đầy đủ các DbSet cho toàn bộ các bảng trong CSDL
        public DbSet<Major> Majors { get; set; }
        public DbSet<Learner> Learner { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Thiết lập tên bảng trong SQL
            modelBuilder.Entity<Major>().ToTable("Major");
            modelBuilder.Entity<Learner>().ToTable("Learner");
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
        }
    }
}