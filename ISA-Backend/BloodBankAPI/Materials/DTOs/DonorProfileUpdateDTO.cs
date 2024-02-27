using BloodBankAPI.Materials.Enums;
using System.ComponentModel.DataAnnotations;

namespace BloodBankAPI.Materials.DTOs
{
    public class DonorProfileUpdateDTO
    {
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string Jmbg { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string Profession { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string Workplace { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string City { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string State { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$",
           ErrorMessage = "Password needs to have at least eight characters, at least one uppercase and one lowercase letter and one number.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        [RegularExpression(@"(male)|(female)",
            ErrorMessage = "Gender can either be male or female.")]
        public string Gender { get; set; }

    }
}
