
namespace BloodBankAPI.Materials.DTOs
{
    public class StaffRegistrationDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public int CenterId { get; set; }

        public StaffRegistrationDTO() { }
    }
}
