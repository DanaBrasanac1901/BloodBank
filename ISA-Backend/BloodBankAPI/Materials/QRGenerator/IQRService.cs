
namespace BloodBankAPI.Materials.QRGenerator
{
    public interface IQRService
    {
        public Byte[] GenerateQR(string data,string fileName);
    }
}
