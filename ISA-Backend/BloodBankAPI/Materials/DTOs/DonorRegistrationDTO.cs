using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankAPI.Materials.DTOs
{
    public class DonorRegistrationDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public long PhoneNumber { get; set; }
        public string Gender { get; set; }
        public long JMBG { get; set; }
        public string Workplace { get; set; }
        public string Profession { get; set; }

        public DonorRegistrationDTO() { }
    }
}
