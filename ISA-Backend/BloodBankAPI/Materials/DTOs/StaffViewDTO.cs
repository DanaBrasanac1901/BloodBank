using BloodBankAPI.Materials.Enums;

namespace BloodBankAPI.Materials.DTOs
{
    public class StaffViewDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public int BloodCenterId { get; set; }
    }
}
