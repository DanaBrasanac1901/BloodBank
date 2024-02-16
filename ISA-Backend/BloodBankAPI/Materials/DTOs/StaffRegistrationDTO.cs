
using System.ComponentModel.DataAnnotations;

namespace BloodBankAPI.Materials.DTOs
{
    public class StaffRegistrationDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]            
        public string Gender { get; set; }
        [Required]
        public int CenterId { get; set; }

        public StaffRegistrationDTO() { }
    }
}
