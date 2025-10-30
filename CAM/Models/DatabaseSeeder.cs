using System;
using System.Linq;
using System.Threading.Tasks;

namespace CAM.Models
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(ClinicDbContext context)
        {
     
            if (context.Doctors.Any() || context.Patients.Any() || context.Appointments.Any())
            {
                return;
            }

         
            var doctors = new[]
            {
                new Doctor
                {
                    ClinicId = 1,
                    FirstName = "Sarah",
                    LastName = "Johnson",
                    Specialty = "General Practice"
                },
                new Doctor
                {
                    ClinicId = 1,
                    FirstName = "Michael",
                    LastName = "Chen",
                    Specialty = "Cardiology"
                },
                new Doctor
                {
                    ClinicId = 1,
                    FirstName = "Emily",
                    LastName = "Rodriguez",
                    Specialty = "Pediatrics"
                },
                new Doctor
                {
                    ClinicId = 1,
                    FirstName = "David",
                    LastName = "Thompson",
                    Specialty = "Orthopedics"
                },
                new Doctor
                {
                    ClinicId = 1,
                    FirstName = "Lisa",
                    LastName = "Park",
                    Specialty = "Dermatology"
                }
            };

            await context.Doctors.AddRangeAsync(doctors);
            await context.SaveChangesAsync();

            
            var schedules = new List<DoctorSchedule>();
            var workingDays = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };

            foreach (var doctor in doctors)
            {
                foreach (var day in workingDays)
                {
                    schedules.Add(new DoctorSchedule
                    {
                        DoctorId = doctor.DoctorId,
                        DayOfWeek = day,
                        StartTime = new TimeSpan(9, 0, 0),
                        EndTime = new TimeSpan(17, 0, 0),  
                        IsWorking = true
                    });
                }
            }

            await context.DoctorSchedules.AddRangeAsync(schedules);
            await context.SaveChangesAsync();

    
            var patients = new[]
            {
                new Patient
                {
                    FirstName = "John",
                    LastName = "Smith",
                    BirthDate = new DateTime(1985, 3, 15),
                    Phone = "+1-555-0101",
                    Email = "john.smith@email.com"
                },
                new Patient
                {
                    FirstName = "Mary",
                    LastName = "Davis",
                    BirthDate = new DateTime(1990, 7, 22),
                    Phone = "+1-555-0102",
                    Email = "mary.davis@email.com"
                },
                new Patient
                {
                    FirstName = "Robert",
                    LastName = "Wilson",
                    BirthDate = new DateTime(1978, 11, 8),
                    Phone = "+1-555-0103",
                    Email = "robert.wilson@email.com"
                },
                new Patient
                {
                    FirstName = "Jennifer",
                    LastName = "Brown",
                    BirthDate = new DateTime(1995, 1, 30),
                    Phone = "+1-555-0104",
                    Email = "jennifer.brown@email.com"
                },
                new Patient
                { 
                    FirstName = "William",
                    LastName = "Taylor",
                    BirthDate = new DateTime(1982, 9, 12),
                    Phone = "+1-555-0105",
                    Email = "william.taylor@email.com"
                },
                new Patient
                {
                    FirstName = "Anna",
                    LastName = "Anderson",
                    BirthDate = new DateTime(2000, 5, 18),
                    Phone = "+1-555-0106",
                    Email = "anna.anderson@email.com"
                },
                new Patient
                {
                    FirstName = "James",
                    LastName = "Miller",
                    BirthDate = new DateTime(1975, 12, 3),
                    Phone = "+1-555-0107",
                    Email = "james.miller@email.com"
                },
                new Patient
                {
                    FirstName = "Linda",
                    LastName = "Garcia",
                    BirthDate = new DateTime(1988, 6, 25),
                    Phone = "+1-555-0108",
                    Email = "linda.garcia@email.com"
                }
            };

            await context.Patients.AddRangeAsync(patients);
            await context.SaveChangesAsync();

           
            var appointments = new List<Appointment>();
            var random = new Random(42); 
            var today = DateTimeOffset.UtcNow.Date;


            for (int dayOffset = 0; dayOffset < 7; dayOffset++)
            {
                var currentDate = today.AddDays(dayOffset);
                var dayOfWeek = currentDate.DayOfWeek;

                if (dayOfWeek != DayOfWeek.Saturday && dayOfWeek != DayOfWeek.Sunday)
                {
                 
                    foreach (var doctor in doctors)
                    {
                        var appointmentsPerDay = random.Next(3, 7);

                        for (int i = 0; i < appointmentsPerDay; i++)
                        {
                            
                            var hour = random.Next(9, 16); 
                            var minute = random.Next(0, 2) * 30;
                            var startTime = currentDate.AddHours(hour).AddMinutes(minute);

                            var patient = patients[random.Next(patients.Length)];

                          
                            var visitLength = random.Next(0, 2) == 0 ? 30 : 60;

                      
                            var statuses = new[] { "Confirmed", "Pending", "Scheduled" };
                            var status = statuses[random.Next(statuses.Length)];

                            appointments.Add(new Appointment
                            {
                                DoctorId = doctor.DoctorId,
                                PatientId = patient.PatientId,
                                StartAt = startTime,
                                EndAt = startTime.AddMinutes(visitLength),
                                VisitLengthMins = visitLength,
                                Status = status,
                                CreatedAt = DateTimeOffset.UtcNow
                            });
                        }
                    }
                }
            }

            await context.Appointments.AddRangeAsync(appointments);
            await context.SaveChangesAsync();
        }
    }
}