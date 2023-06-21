using IronBarCode;
using System.Drawing;
using System.Drawing.Imaging;

namespace BloodBankAPI.Materials.QRGenerator
{
    public class QRService : IQRService
    {
        public byte[] GenerateQR(string data,string fileName)
        {
            GeneratedBarcode barcode = QRCodeWriter.CreateQrCode(data, 200);
            barcode.AddBarcodeValueTextBelowBarcode();
            barcode.SetMargins(10);
            barcode.ChangeBarCodeColor(IronSoftware.Drawing.Color.BlueViolet);
            string filePath = "AppData/" + fileName;
            barcode.SaveAsJpeg(filePath);
            return barcode.BinaryValue;
        }

        /*
         public byte[] GenerateQR(string data,string fileName)
        {
      
            // deprecated????????????
            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            saveToFile(qrCodeImage,fileName);
            return BitmapToBytesCode(qrCodeImage);
           }

    
    private static Byte[] BitmapToBytesCode(Bitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }

        private void saveToFile(Bitmap bitmap,string fileName)
        {
            string filepath = "AppData/"+fileName;
            bitmap.Save(filepath, ImageFormat.Jpeg);
            
        }
        */

    }
}
