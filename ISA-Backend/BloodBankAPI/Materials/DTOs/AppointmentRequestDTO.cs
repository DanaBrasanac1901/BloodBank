namespace BloodBankAPI.Materials.DTOs
{
    public class AppointmentRequestDTO
    {
        public AppointmentRequestDTO() { }


         public int Id { get; set; }
        public string StartDate { get; set; }
        public int Duration { get; set; }
        public int StaffId { get; set; }
        public int CenterId { get; set; }
        public int DonorId { get; set; }

    }
}

