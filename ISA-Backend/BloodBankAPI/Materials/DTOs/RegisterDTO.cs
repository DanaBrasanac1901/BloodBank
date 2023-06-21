namespace BloodBankAPI.Materials.DTOs
{
    public class RegisterDTO
    {
        private string email;
        private string password;
        private string name;
        private string surname;
        private string address;
        private string city;
        private string state;
        private long phoneNum;
        private string gender;
        private long jmbg;
        private string bloodType;
        private string workplace;
        private string employmentInfo;
        private string userType;
        private int idOfCenter;

        public string Email { get { return email; } set { email = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Surname { get { return surname; } set { surname = value; } }
        public string Address { get { return address; } set { address = value; } }
        public string Gender { get { return gender; } set { gender = value; } }
        public long Jmbg { get { return jmbg; } set { jmbg = value; } }
        public string BloodType { get { return bloodType; } set { bloodType = value; } }
        public string Workplace { get { return workplace; } set { workplace = value; } }
        public string EmploymentInfo { get { return employmentInfo; } set { employmentInfo = value; } }
        public string City { get { return city; } set { city = value; } }
        public string State { get { return state; } set { state = value; } }
        public long PhoneNum { get { return phoneNum; } set { phoneNum = value; } }
        public string UserType { get { return userType; } set { userType = value; } }
        public int IdOfCenter { get { return idOfCenter; } set { idOfCenter = value; } }

    }
}
