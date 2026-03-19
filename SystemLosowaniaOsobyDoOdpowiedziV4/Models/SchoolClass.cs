using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemLosowaniaOsobyDoOdpowiedziV4.Models
{
    public class SchoolClass
    {
        public string ClassName { get; set; }
        public List<Students> Students { get; set; } = new List<Students>();
    }
}
