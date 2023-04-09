using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IQrCodeHandler
    {
        public QrCodeEntity GenerateQrCode(QrCodeDto qrCodeDto);
    }
}