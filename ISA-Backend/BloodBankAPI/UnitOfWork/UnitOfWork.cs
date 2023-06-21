using BloodBankAPI.Model;
using BloodBankAPI.Repository;
using BloodBankAPI.Settings;

namespace BloodBankAPI.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IGenericRepository<Account> _accountRepository;
        private readonly IGenericRepository<Donor> _donorRepository;
        private readonly IGenericRepository<Staff> _staffRepository;
        private readonly IGenericRepository<Admin> _adminRepository;
        private readonly IGenericRepository<Question> _questionRepository;
        private readonly IGenericRepository<Form> _formRepository;
        private readonly IGenericRepository<BloodCenter> _bloodCenterRepository;
        private readonly IGenericRepository<CenterAddress> _centerAddressRepository;
        private readonly IGenericRepository<Appointment> _appointmentRepository;
        private readonly IGenericRepository<CancelledAppointment> _cancelledAppointmentRepository;
        private readonly BloodBankDbContext _context;

        public UnitOfWork(IGenericRepository<Account> accountRepository, IGenericRepository<Donor> donorRepository,
            IGenericRepository<Staff> staffRepository, IGenericRepository<Admin> adminRepository,
            IGenericRepository<Question> questionRepository, IGenericRepository<Form> formRepository,
            IGenericRepository<BloodCenter> bloodCenterRepository, IGenericRepository<CenterAddress> centerAddressRepository,
            IGenericRepository<Appointment> appointmentRepository, IGenericRepository<CancelledAppointment> cancelledAppointmentRepository,
            BloodBankDbContext context) { 
            _accountRepository = accountRepository;
            _donorRepository = donorRepository;
            _staffRepository = staffRepository;
            _adminRepository = adminRepository;
            _questionRepository = questionRepository;
            _formRepository = formRepository;
            _appointmentRepository= appointmentRepository;
            _context = context;
            _bloodCenterRepository= bloodCenterRepository;
            _centerAddressRepository= centerAddressRepository;
            _cancelledAppointmentRepository= cancelledAppointmentRepository;
        }

        public IGenericRepository<CenterAddress> AddressRepository { get { return _centerAddressRepository; } }
        public IGenericRepository<BloodCenter> CenterRepository { get { return _bloodCenterRepository; } }
        public IGenericRepository<Account> AccountRepository { get { return _accountRepository; } }
        public IGenericRepository<Donor> DonorRepository { get { return _donorRepository; } }
        public IGenericRepository<Staff> StaffRepository { get { return _staffRepository; } }
        public IGenericRepository<Admin> AdminRepository { get { return _adminRepository; } }
        public IGenericRepository<Appointment> AppointmentRepository { get { return _appointmentRepository; } }
        public IGenericRepository<CancelledAppointment> CancelledAppointmentRepository { get { return _cancelledAppointmentRepository; } }
        public IGenericRepository<Form> FormRepository { get { return _formRepository; } }
        public IGenericRepository<Question> QuestionRepository { get { return _questionRepository; } }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
