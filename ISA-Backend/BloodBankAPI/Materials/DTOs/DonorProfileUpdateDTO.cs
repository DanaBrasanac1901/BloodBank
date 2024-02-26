using BloodBankAPI.Materials.Enums;
using System.ComponentModel.DataAnnotations;

namespace BloodBankAPI.Materials.DTOs
{
    public class DonorProfileUpdateDTO
    {
        public string PhoneNumber { get; set; }
        public string Jmbg { get; set; }
        public string Profession { get; set; }
        public string Workplace { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }

    }
}
