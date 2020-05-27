using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIM
{
    public class MainClass
    {
        static string DataSetPath = ("C:/Users/bestr/Desktop/Repositorios/Iris/Dataset/iris.data");
        List<Flower> Flowers = new List<Flower>();
        List<double> UniqueValues = new List<double>();
        List<double> repeatedValues = new List<double>();

        public MainClass()
        {
            Console.WriteLine(DataSetPath);
            ReadData();
            GetUniqueValues();

            Console.WriteLine();
        }

        void ReadData()
        {
            using (var parser = new TextFieldParser(DataSetPath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();
                    Flower tempFlower = new Flower
                    {
                        SepalLength = double.Parse(fields[0]),
                        SepalWidth = double.Parse(fields[1]),
                        PetalLength = double.Parse(fields[2]),
                        PetalWidth = double.Parse(fields[3]),
                        ClassName = fields[4]
                    };
                    Flowers.Add(tempFlower);
                }
            }
        }

        void GetUniqueValues()
        {
            foreach (var flower in Flowers)
            {
                CheckAttribute(flower.SepalLength);
                CheckAttribute(flower.SepalWidth);
                CheckAttribute(flower.PetalLength);
                CheckAttribute(flower.PetalWidth);
            }
        }

        void CheckAttribute(double value)
        {
            if (!repeatedValues.Contains(value))
            {
                if (UniqueValues.Contains(value))
                {
                    UniqueValues.Remove(value);
                    repeatedValues.Add(value);
                }
                else
                {
                    UniqueValues.Add(value);
                }
            }
        }
    }
}
