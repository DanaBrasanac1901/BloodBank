using BloodBankAPI.Materials.DTOs;
using System.Drawing;
using static QRCoder.PayloadGenerator;

namespace BloodBankAPI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task RegisterDonor(DonorRegistrationDTO dto);
        Task RegisterStaff(StaffRegistrationDTO dto);
        Task RegisterAdmin(AdminRegistrationDTO dto);
        Task<bool> CheckIfEmailExistsAsync(string email);
        Task<bool> EmailMatchesPasswordAsync(LoginDTO dto);
        Task<string> LogInUserAsync(LoginDTO dto);
        Task SendActivationLink(string email, string link);
        Task<bool> ActivateAccount(string email, string token);
        Task<string> PrepareActivationToken(string email);

    }
}
