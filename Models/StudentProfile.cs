using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceMonitoringSystem.Models
{
    public class StudentProfile
    {
        [Key]
        [ForeignKey("Student")]
        public string StudentId { get; set; } = string.Empty;

        public int Age { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        public Student? Student { get; set; }
    }
}
