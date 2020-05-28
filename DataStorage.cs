using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIM
{
    public class DataStorage
    {
        public List<double> Interval { get; set; }
        public String DataStored { get; set; }
        public List<List<double>> Table { get; set; }
        
        public DataStorage(List<double> interval, String data, List<List<double>> table)
        {
            Interval = interval;
            DataStored = data;
            Table = table;
        }
    }
}
