using BloodBankAPI.Materials.Enums;

namespace BloodBankAPI.Materials.DTOs
{
    public class StaffProfileUpdateDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
    }
}
