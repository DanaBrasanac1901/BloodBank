using BloodBankAPI.Model;
using BloodBankAPI.Repository;

namespace BloodBankAPI.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<CenterAddress> AddressRepository { get; }
        IGenericRepository<BloodCenter> CenterRepository { get; }
        IGenericRepository<Account> AccountRepository { get; }
        IGenericRepository<Donor> DonorRepository { get; }
        IGenericRepository<Staff> StaffRepository { get; }
        IGenericRepository<Admin> AdminRepository { get; }
        IGenericRepository<Appointment> AppointmentRepository { get; }
        IGenericRepository<Form> FormRepository { get; }
        IGenericRepository<Question> QuestionRepository { get; }
        IGenericRepository<CancelledAppointment> CancelledAppointmentRepository { get; }

        Task SaveAsync();

    }
}
