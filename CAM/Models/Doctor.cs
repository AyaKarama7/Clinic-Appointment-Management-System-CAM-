namespace CAM.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public int ClinicId { get; set; }      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public ICollection<DoctorSchedule> Schedules { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
