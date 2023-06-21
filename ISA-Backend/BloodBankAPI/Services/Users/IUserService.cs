

using BloodBankAPI.Model;

namespace BloodBankAPI.Services.Users
{
    public interface IUserService
    {
        Task<IEnumerable<Donor>> GetAllDonors();
        Task UpdateDonor(Donor donor);
        Task UpdateStaff(Staff staff);
        Task UpdateAdmin(Admin admin);
        Task<Donor> GetDonorById(int id);
        Task<Staff> GetStaffById(int id);
        Task<Admin> GetAdminById(int id);
        Task DeleteAdmin(Admin admin);
        Task DeleteDonor(Donor donor);
        Task DeleteStaff(Staff staff);


    }
}
