namespace BloodBankAPI.Materials.EmailSender
{
    public interface IEmailSendService
    {
        void SendEmail(Message message);
        void SendWithQR(Message message,byte[] qr,string path);
    }
}
