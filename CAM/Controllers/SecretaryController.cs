using CAM.Models;
using CAM.Services;
using CAM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CAM.Controllers;

public class SecretaryController : Controller
{
    private readonly AppointmentService _service;
    private readonly ClinicDbContext _db;
    public SecretaryController(AppointmentService service, ClinicDbContext db)
    {
        _service = service;
        _db = db;
    }

    [HttpGet]
    public IActionResult New()
    {
        var doctors = _db.Doctors.Select(d => new { d.DoctorId, Name = d.FirstName + " " + d.LastName }).ToList();
        ViewBag.Doctors = new SelectList(doctors, "DoctorId", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AppointmentCreateViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            
            return View("New", vm);
        }


        var (ok, err) = await _service.TryBookAppointmentAsync(vm.DoctorId, new Patient
        {
            FirstName = vm.FirstName,
            LastName = vm.LastName,
            Phone = vm.Phone,
            BirthDate = vm.BirthDate
        }, vm.SlotStart, vm.VisitLengthMins);

        if (!ok)
        {
            ModelState.AddModelError("", err);
            return View("New", vm);
        }

        return RedirectToAction("List"); 
    }


    [HttpGet]
    public async Task<IActionResult> GetFreeSlots(int doctorId, DateTime date, int visitLength = 30)
    {
        var slots = await _service.GetAvailableSlotsAsync(doctorId, date, visitLength);
 
        return Json(slots.Select(s => s.ToString("o")));
    }

    public IActionResult List(DateTime? date)
    {
        var day = date?.Date ?? DateTime.UtcNow.Date;
        var appts = _db.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.StartAt.Date == day)
            .OrderBy(a => a.StartAt)
            .ToList();
        return View(appts);
    }
}

