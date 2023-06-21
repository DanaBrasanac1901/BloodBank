using System.Collections.Generic;

namespace BloodBankAPI.Model
{
    public class Staff : Account
    {
        public virtual BloodCenter BloodCenter { get; set; }
        public int BloodCenterId { get; set; }
        public virtual  ICollection<Appointment> Appointments { get; set; }
        public Staff() {

            Appointments = new List<Appointment>();
        }
    }
}
