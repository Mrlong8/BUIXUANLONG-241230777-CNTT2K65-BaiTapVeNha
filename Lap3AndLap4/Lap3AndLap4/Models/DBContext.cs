using Microsoft.EntityFrameworkCore;
using Lap3AndLap4.Models;


public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    public virtual DbSet<Major> Majors { get; set; }
    public virtual DbSet<Learner> Learners { get; set; }
    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Major>().ToTable("Major");
        modelBuilder.Entity<Learner>().ToTable("Learner");
        modelBuilder.Entity<Course>().ToTable("Course");
        modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
    }
}