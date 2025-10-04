using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AttendanceMonitoringSystem.Models 
{ public class Student 
    { 
        [Key][DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression(@"^\d{2}-\d{6}$", ErrorMessage = "ID must be in format 23-016486")]
        public string Id { get; set; } = null!;
        [Required] 
        public string? Name { get; set; } 
        public DateTime? TimeIn { get; set; } 
        public DateTime? TimeOut { get; set; } 
        public bool? Status { get; set; } 
        public DateTime? AfternoonTimeIn { get; set; } 
        public DateTime? AfternoonTimeOut { get; set; } 
        public bool? AfternoonStatus { get; set; } 
    
    } 
}