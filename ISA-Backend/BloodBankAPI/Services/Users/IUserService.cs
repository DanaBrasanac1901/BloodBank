

using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Model;

namespace BloodBankAPI.Services.Users
{
    public interface IUserService
    {
        Task<IEnumerable<Donor>> GetAllDonors();
        Task UpdateDonor(DonorProfileUpdateDTO donor);
        Task UpdateStaff(StaffProfileUpdateDTO staff);
        Task UpdateAdmin(Admin admin);
        Task<Donor> GetDonorById(int id);
        Task<Staff> GetStaffById(int id);
        Task<Admin> GetAdminById(int id);
        Task DeleteAdmin(Admin admin);
        Task DeleteDonor(Donor donor);
        Task DeleteStaff(Staff staff);


    }
}
