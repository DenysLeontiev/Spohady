using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace API.Controllers
{
    [Authorize]
    public class QrCodeController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IQrCodeHandler _qrCodeHandler;

        public QrCodeController(IQrCodeHandler qrCodeHandler, UserManager<AppUser> userManager)
        {
            _qrCodeHandler = qrCodeHandler;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<QrCodeEntity> GenerateQR([FromBody] QrCodeDto qrCodeDto)
        {
            QrCodeEntity qrCodeEntity = _qrCodeHandler.GenerateQrCode(qrCodeDto);
            return qrCodeEntity;
        }
    }
}