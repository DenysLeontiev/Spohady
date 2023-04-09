using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using QRCoder;

namespace API.Services
{
    public class QrCodeHandler : IQrCodeHandler
    {
        public QrCodeEntity GenerateQrCode(QrCodeDto qrCodeDto)
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(qrCodeDto.QrData, QRCodeGenerator.ECCLevel.Q);

            QRCode qRCode = new QRCode(qRCodeData);
            Bitmap qrCodeImage = qRCode.GetGraphic(20);
            // Bitmap qrCodeImage = qRCode.GetGraphic(20, Color.White, Color.Black, true);

            MemoryStream ms = new MemoryStream();
            qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            byte[] byteQrArray = ms.ToArray();
            string qrBase64 = Convert.ToBase64String(byteQrArray);

            return new QrCodeEntity 
            {
                Id = 1,
                QrCodeBase64 = qrBase64,
            };
        }
    }
}