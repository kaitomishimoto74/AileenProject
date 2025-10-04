using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AttendanceMonitoringSystem.Models
{
    public class AttendanceSelectViewModel
    {
        public string SelectedStudentId { get; set; }
        public List<IdentityUser> RegisteredStudents { get; set; }
    }
}