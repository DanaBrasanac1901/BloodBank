

using System.ComponentModel.DataAnnotations;

namespace BloodBankAPI.Materials.DTOs
{
    public class AdminRegistrationDTO
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

        public AdminRegistrationDTO()
        {

        }
    }
}
