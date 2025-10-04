using Microsoft.EntityFrameworkCore;
namespace AttendanceMonitoringSystem.Models 
{ 
    public class StudentAttendanceDbContext : DbContext 
    { 
        public DbSet<Student> Attendance { get; set; } 
        
        public DbSet<StudentProfile> StudentProfiles { get; set; } 
        public StudentAttendanceDbContext(DbContextOptions<StudentAttendanceDbContext> options) 
            : base(options) { 
        }
    } 
}