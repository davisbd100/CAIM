using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIM
{
    public class CAIMArray
    {
        public List<double> Ranges { get; set; }
        public double CAIMValue { get; set; }
        public double AddedValue { get; set; }


        public CAIMArray(List<double> list, double caimValue, double addedValue)
        {
            Ranges = list;
            CAIMValue = caimValue;
            AddedValue = addedValue;
        }
        public CAIMArray()
        {
        }
    }
}
