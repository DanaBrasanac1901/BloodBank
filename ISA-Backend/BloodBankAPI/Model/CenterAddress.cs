using System;
using System.IO;

namespace BloodBankAPI.Model
{
    public class CenterAddress : Entity
    {
 
        public CenterAddress()
        {
        }

        public string City { get; set; }
        public string Country { get; set; }
        public string StreetAddress { get; set; }

        public virtual BloodCenter BloodCenter { get; set; }
        public int CenterId { get; set; }

        public override string ToString()
        {
            return StreetAddress + "," + City + "," + Country;
        }
    }
}
