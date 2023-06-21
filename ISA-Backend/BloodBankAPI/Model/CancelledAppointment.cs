using BloodBankAPI.Materials.Enums;

namespace BloodBankAPI.Model
{
    public class CancelledAppointment: Entity
    {
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public AppointmentStatus Status { get; set; }
        public byte[]? QrCode { get; set; }
        public int CenterId { get; set; }
        public int DonorId { get; set; }
        public int StaffId { get; set; }
        public virtual Donor Donor { get; set; }
    }
}
