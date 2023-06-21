namespace BloodBankAPI.Materials.DTOs
{
    public class GeneratePredefinedAppointmentDTO
    {
        public string StartDate { get; set; }
        public int Duration { get; set; }
        public int StaffId { get; set; }
        public int CenterId { get; set; }

        public string Status { get; set; }

        public GeneratePredefinedAppointmentDTO() { }
    }
}
