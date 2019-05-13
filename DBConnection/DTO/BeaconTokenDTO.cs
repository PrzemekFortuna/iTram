using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnection.DTO
{
    public class BeaconTokenDto
    {
        public byte[] Token { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidThrough { get; set; }
    }
}
