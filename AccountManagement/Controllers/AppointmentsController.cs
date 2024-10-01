using Microsoft.AspNetCore.Mvc;
using AccountManagement.Data;
using AccountManagement.Models;
using System.Linq;

namespace AccountManagement.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public IActionResult Index()
        {
            var appointments = _context.Appointments.ToList();
            return View(appointments);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                if (appointment.AppointmentDateTime <= DateTime.Now)
                {
                    ModelState.AddModelError("", "Appointment date and time must be in the future.");
                    return View(appointment);
                }

                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(appointment);
        }
    }
}
