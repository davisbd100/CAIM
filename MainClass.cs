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
            CheckCaimValue();

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
            UniqueValues.OrderBy(i => i);
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

        void CheckCaimValue()
        {
            int k = 0;
            List<double> bestValues = new List<double>();
            List<double> tempUniqueValues = new List<double>();
            tempUniqueValues.Add(UniqueValues.First());
            tempUniqueValues.Add(UniqueValues.Last());
            CAIMArray tempCAIMArray = new CAIMArray(tempUniqueValues, CalculateCAIM(tempUniqueValues));4



            Console.WriteLine();
        }








        double CalculateCAIM(List<double> list)
        {
            double result = 0;
            list = list.OrderBy(i => i).ToList();

            int cont = 0;
            List<List<double>> table = new List<List<double>>();
            table.Add(new List<double>());
            table.Add(new List<double>());
            table.Add(new List<double>());
            for (int i = 0; i < list.Count - 1; i++)
            {
                foreach (var listas in table)
                {
                    listas.Add(0);
                }
                double min = list[i];
                double max = list[i + 1];
                cont++;
                foreach (var flower in Flowers)
                {
                    switch (flower.ClassName)
                    {
                        case "Iris-setosa":
                            if (flower.PetalLength > min && flower.PetalLength < max)
                            {
                                table[0][i] = table[0][i] + 1;
                            }
                            if (flower.PetalWidth > min && flower.PetalWidth < max)
                            {
                                table[0][i] = table[0][i] + 1;
                            }
                            if (flower.SepalLength > min && flower.SepalLength < max)
                            {
                                table[0][i] = table[0][i] + 1;
                            }
                            if (flower.SepalWidth > min && flower.SepalWidth < max)
                            {
                                table[0][i] = table[0][i] + 1;
                            }
                            break;
                        case "Iris-versicolor":
                            if (flower.PetalLength > min && flower.PetalLength < max)
                            {
                                table[1][i] = table[1][i] + 1;
                            }
                            if (flower.PetalWidth > min && flower.PetalWidth < max)
                            {
                                table[1][i] = table[1][i] + 1;
                            }
                            if (flower.SepalLength > min && flower.SepalLength < max)
                            {
                                table[1][i] = table[1][i] + 1;
                            }
                            if (flower.SepalWidth > min && flower.SepalWidth < max)
                            {
                                table[1][i] = table[1][i] + 1;
                            }
                            break;
                        case "Iris-virginica":
                            if (flower.PetalLength > min && flower.PetalLength < max)
                            {
                                table[2][i] = table[2][i] + 1;
                            }
                            if (flower.PetalWidth > min && flower.PetalWidth < max)
                            {
                                table[2][i] = table[2][i] + 1;
                            }
                            if (flower.SepalLength > min && flower.SepalLength < max)
                            {
                                table[2][i] = table[2][i] + 1;
                            }
                            if (flower.SepalWidth > min && flower.SepalWidth < max)
                            {
                                table[2][i] = table[2][i] + 1;
                            }
                            break;
                    }
                }
            }
            List<double> verticalresult = new List<double>();
            for (int i = 0; i < table[0].Count; i++)
            {
                double sum = table[0][i] + table[1][i] + table[2][i];
                Math.Max(table[0][i], Math.Max(table[1][i], table[2][i]));
                verticalresult.Add(sum);
            }
            table.Add(verticalresult);
            foreach (var lista in table)
            {
                double sum = 0;
                foreach (var Numero in lista)
                {
                    sum += Numero;
                }
                lista.Add(sum);
            }

            for (int i = 0; i < table[0].Count; i++)
            {
                double maxColumnValue = Math.Max(table[0][i], Math.Max(table[1][i], table[2][i]));
                result += (Math.Pow(maxColumnValue, 2)) / table[3][i];
            }
            result /= cont;
            Console.WriteLine();
            return result;
        }
    }
}
