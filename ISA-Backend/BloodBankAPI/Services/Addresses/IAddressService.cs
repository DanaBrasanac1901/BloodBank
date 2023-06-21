using BloodBankAPI.Model;
namespace BloodBankAPI.Services.Addresses
{
    public interface IAddressService
    {
        Task<IEnumerable<CenterAddress>> GetAllAsync();
        Task<CenterAddress> GetByIdAsync(int id);
        Task<CenterAddress> GetByCenterAsync(int centerId);
        Task<IEnumerable<string>> GetCitiesAsync();
        Task Create(CenterAddress address);
        Task Update(CenterAddress address);
        Task Delete(CenterAddress address);
    }
}
