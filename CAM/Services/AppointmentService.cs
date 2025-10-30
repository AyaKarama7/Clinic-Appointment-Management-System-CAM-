using CAM.Models;
using Microsoft.EntityFrameworkCore;

namespace CAM.Services;

public class AppointmentService
{
    private readonly ClinicDbContext _db;
    public AppointmentService(ClinicDbContext db) { _db = db; }

    public async Task<List<DateTimeOffset>> GetAvailableSlotsAsync(int doctorId, DateTime date, int visitLengthMins = 30)
    {

        var dayOfWeek = date.DayOfWeek;
        var schedule = await _db.DoctorSchedules
            .Where(s => s.DoctorId == doctorId && s.DayOfWeek == dayOfWeek && s.IsWorking)
            .ToListAsync();

        if (!schedule.Any()) return new List<DateTimeOffset>();

        List<(TimeSpan Start, TimeSpan End)> blocks = schedule
            .Select(s => (s.StartTime, s.EndTime)).ToList();


        var dayStart = new DateTimeOffset(date.Year, date.Month, date.Day, 0, 0, 0, TimeSpan.Zero);
        var dayEnd = dayStart.AddDays(1);

        var existing = await _db.Appointments
            .Where(a => a.DoctorId == doctorId && a.StartAt >= dayStart && a.StartAt < dayEnd)
            .ToListAsync();

        var freeSlots = new List<DateTimeOffset>();
        foreach (var block in blocks)
        {
            var cursor = date.Date + block.Start;
            var blockEnd = date.Date + block.End;

            while (cursor.AddMinutes(visitLengthMins) <= blockEnd)
            {
                var slotStart = new DateTimeOffset(cursor, TimeSpan.Zero); 
                var slotEnd = slotStart.AddMinutes(visitLengthMins);

                bool hasConflict = existing.Any(e => slotStart < e.EndAt && e.StartAt < slotEnd);
                if (!hasConflict) freeSlots.Add(slotStart);

                cursor = cursor.AddMinutes(visitLengthMins);
            }
        }
        return freeSlots;
    }

    public async Task<(bool success, string error)> TryBookAppointmentAsync(int doctorId, Patient patientDto, DateTimeOffset slotStart, int visitLengthMins = 30)
    {
        using var tx = await _db.Database.BeginTransactionAsync();
        try
        {

            Patient patient = await _db.Patients.FirstOrDefaultAsync(p => p.Phone == patientDto.Phone && p.FirstName == patientDto.FirstName && p.LastName == patientDto.LastName);
            if (patient == null)
            {
                patient = patientDto;
                _db.Patients.Add(patient);
                await _db.SaveChangesAsync();
            }

            var slotEnd = slotStart.AddMinutes(visitLengthMins);

            var conflict = await _db.Appointments.AnyAsync(a => a.DoctorId == doctorId && slotStart < a.EndAt && a.StartAt < slotEnd);
            if (conflict) return (false, "Slot already taken");

            var appointment = new Appointment
            {
                DoctorId = doctorId,
                PatientId = patient.PatientId,
                StartAt = slotStart,
                EndAt = slotEnd,
                VisitLengthMins = visitLengthMins,
                Status = "Scheduled",
                CreatedAt = DateTimeOffset.UtcNow
            };
            _db.Appointments.Add(appointment);
            await _db.SaveChangesAsync();
            await tx.CommitAsync();
            return (true, null);
        }
        catch (Exception ex)
        {
            await tx.RollbackAsync();
            return (false, ex.Message);
        }
    }
}
