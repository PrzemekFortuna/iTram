using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnection.Entities
{
    public class Tram
    {
        public int TramId { get; set; }
        public int CityId { get; set; }
        public int Number { get; set; }

        public virtual City City { get; set; }
    }
}
