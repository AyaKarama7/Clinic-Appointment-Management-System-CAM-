namespace CAM.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public int? PatientId { get; set; }
        public DateTimeOffset StartAt { get; set; }
        public DateTimeOffset EndAt { get; set; }
        public int VisitLengthMins { get; set; } = 30;
        public string Status { get; set; } = "Scheduled";
        public byte[] RowVersion { get; set; }  
        public Doctor Doctor { get; set; }
        public Patient? Patient { get; set; }
        public DateTimeOffset CreatedAt { get; internal set; }
    }
}
