using AttendanceMonitoringSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace AttendanceMonitoringSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentAttendanceDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, 
            StudentAttendanceDbContext context,
            UserManager<IdentityUser> userManager) 
        { 
          _logger = logger;
          _context = context;
          _userManager = userManager;
        }
        [Authorize(Roles = "Instructor, Student")]
        public IActionResult Index ()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize(Roles = "Instructor")]
        public IActionResult Attendance()
        {
            var allAttendance = _context.Attendance.ToList();
            return View(allAttendance);
        }
        public IActionResult StudentAdding(string? id)
        {
            ViewBag.RegisteredStudents = _userManager.Users.ToList();

            if (id  != null) {
                var studentInDb = _context.Attendance.SingleOrDefault(student => student.Id == id);
                return View(studentInDb);
            }
            return View();
        }
        public IActionResult StudentStatusEditing(string? id)
        {
            if (id != null) {
                var student = _context.Attendance.SingleOrDefault(s => s.Id == id);
                if (student == null) return NotFound();
                var profile = _context.StudentProfiles.SingleOrDefault(p => p.StudentId == id) ?? new StudentProfile { StudentId = id };
                ViewBag.StudentName = student.Name;
                return View(profile);
            }
            return NotFound();
}
        public IActionResult DeleteStudent(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Student ID is required.");
            }
            var studentInDb = _context.Attendance.SingleOrDefault(student => student.Id == id);

            if (studentInDb == null)
            {
                return NotFound();
            }
            _context.Attendance.Remove(studentInDb);
            _context.SaveChanges();

            return RedirectToAction("Attendance");
        }
        [HttpPost]
        public IActionResult StudentAddingForm(Student model)
        {
            if (!ModelState.IsValid)
            {
                return View("StudentAdding", model);
            }
            var exists = _context.Attendance.Any(s => s.Id == model.Id);
            if (exists)
            {
                ModelState.AddModelError("Id", "Student ID already exists");
                return View("StudentAdding", model);
            }
            _context.Attendance.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Attendance");
        }
        [HttpPost]
        public IActionResult StudentEditingForm(Student model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                if (model.Status == true)
                    model.TimeIn = DateTime.Now;

                model.TimeOut = DateTime.Today.AddHours(12);
                _context.Attendance.Add(model);
            }
            else
            {
                var studentInDb = _context.Attendance.SingleOrDefault(s => s.Id == model.Id);

                if (studentInDb != null)
                {
                    studentInDb.Status = model.Status;

                    if (model.Status == true)
                        studentInDb.TimeIn = DateTime.Now;

                    else
                        studentInDb.TimeIn = null;

                    studentInDb.TimeOut = DateTime.Today.AddHours(12);
                    _context.Attendance.Update(studentInDb);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Attendance");
        }
        [HttpPost]
        public IActionResult ToggleStatus(string id)
        {
            var student = _context.Attendance.SingleOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            if (student.Status == true)
            {
                student.Status = false;
                student.TimeIn = null;
                student.TimeOut = null;
            }
            else
            {
                student.Status = true;
                student.TimeIn = DateTime.Now;
                student.TimeOut = DateTime.Today.AddHours(12);  
            }
            _context.SaveChanges();
            return RedirectToAction("Attendance");
        }
        [HttpPost]
        public IActionResult ToggleAfternoonStatus(string id)
        {
            var student = _context.Attendance.SingleOrDefault(s => s.Id == id);
            if (student == null) return NotFound();

            if (student.AfternoonStatus == true)
            {
                student.AfternoonStatus = false;
                student.AfternoonTimeIn = null;
                student.AfternoonTimeOut = null;
            }
            else
            {
                student.AfternoonStatus = true;
                student.AfternoonTimeIn = DateTime.Now;
                student.AfternoonTimeOut = DateTime.Today.AddHours(17);
            }
            _context.SaveChanges();
            return RedirectToAction("Attendance");
        }
        [HttpPost]
        public IActionResult SaveStudentProfile(StudentProfile profile)
        {
            var student = _context.Attendance.FirstOrDefault(s => s.Id == profile.StudentId);
            if (student != null)
            {
                var newName = Request.Form["StudentName"];
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    student.Name = newName;
                }
            }
            var existingProfile = _context.StudentProfiles.FirstOrDefault(p => p.StudentId == profile.StudentId);
            if (existingProfile != null)
            {
                existingProfile.Age = profile.Age;
                existingProfile.Address = profile.Address;
                _context.StudentProfiles.Update(existingProfile);
            }
            else
            {
                _context.StudentProfiles.Add(profile);
            }
            _context.SaveChanges();
            return RedirectToAction("Attendance");
        }
        [Authorize(Roles = "Student")]
    public IActionResult MyAttendance()
{
    var attendance = _context.Attendance.ToList();
    return View(attendance);
}
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AddAttendance()
        {
            var model = new AttendanceSelectViewModel
            {
                RegisteredStudents = _userManager.Users.ToList()
            };
            return View(model);
        }
    }
}