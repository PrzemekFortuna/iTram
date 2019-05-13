using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnection.Entities
{
    public class BeaconToken
    {
        public int Id { get; protected set; }
        public byte[] Token { get; protected set; }
        public DateTime ValidFrom { get; protected set; }
        public DateTime ValidThrough { get; protected set; }

        public BeaconToken(byte[] token, DateTime validFrom, DateTime validThrough)
        {
            Token = token;
            ValidFrom = validFrom;
            ValidThrough = validThrough;
        }
    }
}
