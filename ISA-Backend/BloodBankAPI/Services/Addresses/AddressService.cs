
using BloodBankAPI.Model;
using BloodBankAPI.UnitOfWork;

namespace BloodBankAPI.Services.Addresses
{
    public class AddressService: IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
       
        public AddressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CenterAddress>> GetAllAsync()
        {
            return await _unitOfWork.AddressRepository.GetAllAsync();
        }

        //ovo nekako popraviti
        public async Task<IEnumerable<string>> GetCitiesAsync()
        {
            List<string> cities = new List<string>();
            foreach (CenterAddress address in await _unitOfWork.AddressRepository.GetAllAsync())
            {
                if (!cities.Contains(address.City)) cities.Add(address.City);
            }
            return cities;
        }

        public async Task<CenterAddress> GetByIdAsync(int id)
        {

            return await _unitOfWork.AddressRepository.GetByIdAsync(id);
        }

        public async Task<CenterAddress> GetByCenterAsync(int centerId)
        {
           IEnumerable<CenterAddress> addresses = await _unitOfWork.AddressRepository.GetByConditionAsync(address => address.CenterId == centerId);
           return addresses.FirstOrDefault();
        }

        public async Task Create(CenterAddress address)
        {
            await _unitOfWork.AddressRepository.InsertAsync(address);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(CenterAddress address)
        {
            _unitOfWork.AddressRepository.Update(address);
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(CenterAddress address)
        {
            _unitOfWork.AddressRepository.Delete(address);
            await _unitOfWork.SaveAsync();
        }
    }
}
