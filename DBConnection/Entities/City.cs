using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnection.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Tram> Trams { get; set; }
    }
}
