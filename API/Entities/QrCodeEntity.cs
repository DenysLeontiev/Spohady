using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class QrCodeEntity
    {
        public int Id { get; set; }
        public string QrCodeBase64 { get; set; }
    }
}