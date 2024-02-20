using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBankAPI.Model
{
    public class Donor : Account
    {
        
        public string PhoneNumber { get; set; }
        public string Jmbg { get; set; }
        public string Profession { get; set; }
        public string Workplace { get; set; }

        public string Address { get; set; }

        //broj penala - nepojavljivanje na pregledu
        public int Strikes { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<CancelledAppointment> CancelledAppointments { get; set; }

        public Donor()
        {
            Appointments = new List<Appointment>();
        }
    }
}
