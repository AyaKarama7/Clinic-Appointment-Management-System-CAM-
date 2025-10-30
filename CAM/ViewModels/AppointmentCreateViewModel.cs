using System.ComponentModel.DataAnnotations;

namespace CAM.ViewModels;

public class AppointmentCreateViewModel
{

    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public DateTime BirthDate { get; set; }
    [Required] public string Phone { get; set; }


    [Required] public int DoctorId { get; set; }
    [Required] public DateTimeOffset SlotStart { get; set; }  
    public int VisitLengthMins { get; set; } = 30;
   // public string Email { get; internal set; }
}

