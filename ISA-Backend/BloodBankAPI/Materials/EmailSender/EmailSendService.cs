using MimeKit;
using MailKit.Net.Smtp;

namespace BloodBankAPI.Materials.EmailSender
{

    public class EmailSendService : IEmailSendService
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSendService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        public void SendWithQR(Message message, byte[] arr,string path)
        {
           
            MimeMessage _message=ConstructAttachment(CreateEmailMessage(message), path);
            Send(_message);

        }
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("BloodBank", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();



            client.Connect("smtp.gmail.com", _emailConfig.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

            client.Send(mailMessage);



            client.Disconnect(true);
            client.Dispose();


        }

        private MimeMessage ConstructAttachment(MimeMessage message,string path)
        {

            var body = new TextPart("plain")
            {
                Text = @"Please scan the attached QR code for more information:"
            };

            MimeContent content = new MimeContent(File.OpenRead(path), ContentEncoding.Default);

            var attachment = new MimePart("image", "gif")

            {
                Content = content,
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(path)
            };

            //File.Create(path).Close();

            var multipart = new Multipart("mixed");
            multipart.Add(body);
            multipart.Add(attachment);

            message.Body = multipart;
            return message;
        }
    }
}
