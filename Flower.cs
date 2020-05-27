using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIM
{
    public class Flower
    {
        public double SepalLength { get; set; }
        public double SepalWidth { get; set; }
        public double PetalLength { get; set; }
        public double PetalWidth { get; set; }
        public String ClassName { get; set; }

        public Flower(double sepalLength, double sepalWidth, double petalLength, double petalWidth, String className)
        {
            this.SepalLength = sepalLength;
            this.SepalWidth = sepalWidth;
            this.PetalLength = petalLength;
            this.PetalWidth = petalWidth;
            this.ClassName = className;
        }
    }
}
