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

        public MainClass()
        {
            ReadData();
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



        public List<DataStorage> AlgorithOrchestrator()
        {
            List<DataStorage> datas = new List<DataStorage>();

            datas.Add(CheckCaimValue(Type.SepalLength));
            datas.Add(CheckCaimValue(Type.SepalWidth));
            datas.Add(CheckCaimValue(Type.PetalLength));
            datas.Add(CheckCaimValue(Type.PetalWidth));

            return datas;
        }

        void GetUniqueValues(Type type)
        {
            UniqueValues = new List<double>();
            switch (type)
            {
                case (Type.SepalLength):
                    foreach (var flower in Flowers)
                    {
                        CheckAttribute(flower.SepalLength);
                    }
                    break;
                case (Type.SepalWidth):
                    foreach (var flower in Flowers)
                    {
                        CheckAttribute(flower.SepalWidth);
                    }
                    break;
                case (Type.PetalLength):
                    foreach (var flower in Flowers)
                    {
                        CheckAttribute(flower.PetalLength);
                    }
                    break;
                case (Type.PetalWidth):
                    foreach (var flower in Flowers)
                    {
                        CheckAttribute(flower.PetalWidth);
                    }
                    break;
            }
            UniqueValues = UniqueValues.OrderBy(j => j).ToList();
        }

        void CheckAttribute(double value)
        {
            if (!UniqueValues.Contains(value))
            {
                UniqueValues.Add(value);
            }
        }


        enum Type
        {
            SepalLength,
            SepalWidth,
            PetalLength,
            PetalWidth
        }
        DataStorage CheckCaimValue(Type type)
        {
            GetUniqueValues(type);
            List<double> bestValues = new List<double>();
            List<double> tempUniqueValues = new List<double>();
            List<double> backupUniqueValues = UniqueValues.ToList();
            tempUniqueValues.Add(backupUniqueValues.First());
            tempUniqueValues.Add(backupUniqueValues.Last());
            backupUniqueValues.Remove(backupUniqueValues.First());
            backupUniqueValues.Remove(backupUniqueValues.Last());
            tempUniqueValues = tempUniqueValues.OrderBy(i => i).ToList();
            int k = 1;
            double globalValue = 0;
            while (backupUniqueValues.Any())
            {
                List<CAIMArray> listCAIM = new List<CAIMArray>();

                for (int i = 0; i < backupUniqueValues.Count; i++)
                {
                    List<double> aidValues = tempUniqueValues.ToList();
                    aidValues.Add(backupUniqueValues[i]);
                    aidValues = aidValues.OrderBy(j => j).ToList();
                    listCAIM.Add(new CAIMArray(aidValues, CalculateCAIM(aidValues, type), backupUniqueValues[i]));
                }
                listCAIM = listCAIM.OrderBy(j => j.CAIMValue).ToList();
                CAIMArray saveCAIM = listCAIM.Last();

                if (saveCAIM.CAIMValue > globalValue || k < 3)
                {
                    globalValue = saveCAIM.CAIMValue;
                    tempUniqueValues.Add(saveCAIM.AddedValue);
                    tempUniqueValues = tempUniqueValues.OrderBy(i => i).ToList();
                    backupUniqueValues.Remove(saveCAIM.AddedValue);
                    k++;
                }
                else
                {
                    break;
                }
            }

            DataStorage data = new DataStorage(tempUniqueValues, type.ToString(), GenerateCountTable(tempUniqueValues, type));
            return data;
        }

        double CalculateCAIM(List<double> list, Type type)
        {
            double result = 0;
            int cont = list.Count - 1;
            List<List<double>> table = new List<List<double>>();
            table.Add(new List<double>());
            table.Add(new List<double>());
            table.Add(new List<double>()); 
            switch (type)
            {
                case (Type.SepalLength):
                    for (int i = 0; i < list.Count - 1; i++)
                    {
                        foreach (var listas in table)
                        {
                            listas.Add(0);
                        }
                        double min = list[i];
                        double max = list[i + 1];
                        foreach (var flower in Flowers)
                        {
                            switch (flower.ClassName)
                            {
                                case "Iris-setosa":
                                    if (flower.SepalLength >= min && flower.SepalLength < max)
                                    {
                                        table[0][i] = table[0][i] + 1;
                                    }
                                    break;
                                case "Iris-versicolor":
                                    if (flower.SepalLength >= min && flower.SepalLength < max)
                                    {
                                        table[1][i] = table[1][i] + 1;
                                    }
                                    break;
                                case "Iris-virginica":
                                    if (flower.SepalLength >= min && flower.SepalLength < max || (max == 7.9 && flower.SepalLength == 7.9))
                                    {
                                        table[2][i] = table[2][i] + 1;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case (Type.SepalWidth):
                    for (int i = 0; i < list.Count - 1; i++)
                    {
                        foreach (var listas in table)
                        {
                            listas.Add(0);
                        }
                        double min = list[i];
                        double max = list[i + 1];
                        foreach (var flower in Flowers)
                        {
                            switch (flower.ClassName)
                            {
                                case "Iris-setosa":
                                    if (flower.SepalWidth >= min && flower.SepalWidth < max)
                                    {
                                        table[0][i] = table[0][i] + 1;
                                    }
                                    break;
                                case "Iris-versicolor":
                                    if (flower.SepalWidth >= min && flower.SepalWidth < max)
                                    {
                                        table[1][i] = table[1][i] + 1;
                                    }
                                    break;
                                case "Iris-virginica":
                                    if (flower.SepalWidth >= min && flower.SepalWidth < max)
                                    {
                                        table[2][i] = table[2][i] + 1;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case (Type.PetalLength):
                    for (int i = 0; i < list.Count - 1; i++)
                    {
                        foreach (var listas in table)
                        {
                            listas.Add(0);
                        }
                        double min = list[i];
                        double max = list[i + 1];
                        foreach (var flower in Flowers)
                        {
                            switch (flower.ClassName)
                            {
                                case "Iris-setosa":
                                    if (flower.PetalLength >= min && flower.PetalLength < max)
                                    {
                                        table[0][i] = table[0][i] + 1;
                                    }
                                    break;
                                case "Iris-versicolor":
                                    if (flower.PetalLength >= min && flower.PetalLength < max)
                                    {
                                        table[1][i] = table[1][i] + 1;
                                    }
                                    break;
                                case "Iris-virginica":
                                    if (flower.PetalLength >= min && flower.PetalLength < max)
                                    {
                                        table[2][i] = table[2][i] + 1;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case (Type.PetalWidth):
                    for (int i = 0; i < list.Count - 1; i++)
                    {
                        foreach (var listas in table)
                        {
                            listas.Add(0);
                        }
                        double min = list[i];
                        double max = list[i + 1];
                        foreach (var flower in Flowers)
                        {
                            switch (flower.ClassName)
                            {
                                case "Iris-setosa":
                                    if (flower.PetalWidth >= min && flower.PetalWidth < max)
                                    {
                                        table[0][i] = table[0][i] + 1;
                                    }
                                    break;
                                case "Iris-versicolor":
                                    if (flower.PetalWidth >= min && flower.PetalWidth < max)
                                    {
                                        table[1][i] = table[1][i] + 1;
                                    }
                                    break;
                                case "Iris-virginica":
                                    if (flower.PetalWidth >= min && flower.PetalWidth < max)
                                    {
                                        table[2][i] = table[2][i] + 1;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
            }
            List<double> verticalresult = new List<double>();
            for (int i = 0; i < table[0].Count; i++)
            {
                double sum = table[0][i] + table[1][i] + table[2][i];
                verticalresult.Add(sum);
            }
            table.Add(verticalresult);

            for (int i = 0; i < table[0].Count; i++)
            {
                double maxColumnValue = Math.Max(table[0][i], Math.Max(table[1][i], table[2][i]));
                double divValue = table[3][i];
                result += (Math.Pow(maxColumnValue, 2)) / divValue;
            }

            foreach (var lista in table)
            {
                double sum = 0;
                foreach (var Numero in lista)
                {
                    sum += Numero;
                }
                lista.Add(sum);
            }
            result /= cont;
            return result;
        }

        List<List<double>> GenerateCountTable(List<double> list, Type type)
        {
            int cont = list.Count - 1;
            List<List<double>> table = new List<List<double>>();
            table.Add(new List<double>());
            table.Add(new List<double>());
            table.Add(new List<double>());
            switch (type)
            {
                case (Type.SepalLength):
                    for (int i = 0; i < list.Count - 1; i++)
                    {
                        foreach (var listas in table)
                        {
                            listas.Add(0);
                        }
                        double min = list[i];
                        double max = list[i + 1];
                        foreach (var flower in Flowers)
                        {
                            switch (flower.ClassName)
                            {
                                case "Iris-setosa":
                                    if (flower.SepalLength >= min && flower.SepalLength < max)
                                    {
                                        table[0][i] = table[0][i] + 1;
                                    }
                                    break;
                                case "Iris-versicolor":
                                    if (flower.SepalLength >= min && flower.SepalLength < max)
                                    {
                                        table[1][i] = table[1][i] + 1;
                                    }
                                    break;
                                case "Iris-virginica":
                                    if (flower.SepalLength >= min && flower.SepalLength < max || (max == 7.9 && flower.SepalLength == 7.9))
                                    {
                                        table[2][i] = table[2][i] + 1;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case (Type.SepalWidth):
                    for (int i = 0; i < list.Count - 1; i++)
                    {
                        foreach (var listas in table)
                        {
                            listas.Add(0);
                        }
                        double min = list[i];
                        double max = list[i + 1];
                        foreach (var flower in Flowers)
                        {
                            switch (flower.ClassName)
                            {
                                case "Iris-setosa":
                                    if (flower.SepalWidth >= min && flower.SepalWidth < max)
                                    {
                                        table[0][i] = table[0][i] + 1;
                                    }
                                    break;
                                case "Iris-versicolor":
                                    if (flower.SepalWidth >= min && flower.SepalWidth < max)
                                    {
                                        table[1][i] = table[1][i] + 1;
                                    }
                                    break;
                                case "Iris-virginica":
                                    if (flower.SepalWidth >= min && flower.SepalWidth < max)
                                    {
                                        table[2][i] = table[2][i] + 1;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case (Type.PetalLength):
                    for (int i = 0; i < list.Count - 1; i++)
                    {
                        foreach (var listas in table)
                        {
                            listas.Add(0);
                        }
                        double min = list[i];
                        double max = list[i + 1];
                        foreach (var flower in Flowers)
                        {
                            switch (flower.ClassName)
                            {
                                case "Iris-setosa":
                                    if (flower.PetalLength >= min && flower.PetalLength < max)
                                    {
                                        table[0][i] = table[0][i] + 1;
                                    }
                                    break;
                                case "Iris-versicolor":
                                    if (flower.PetalLength >= min && flower.PetalLength < max)
                                    {
                                        table[1][i] = table[1][i] + 1;
                                    }
                                    break;
                                case "Iris-virginica":
                                    if (flower.PetalLength >= min && flower.PetalLength < max)
                                    {
                                        table[2][i] = table[2][i] + 1;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
                case (Type.PetalWidth):
                    for (int i = 0; i < list.Count - 1; i++)
                    {
                        foreach (var listas in table)
                        {
                            listas.Add(0);
                        }
                        double min = list[i];
                        double max = list[i + 1];
                        foreach (var flower in Flowers)
                        {
                            switch (flower.ClassName)
                            {
                                case "Iris-setosa":
                                    if (flower.PetalWidth >= min && flower.PetalWidth < max)
                                    {
                                        table[0][i] = table[0][i] + 1;
                                    }
                                    break;
                                case "Iris-versicolor":
                                    if (flower.PetalWidth >= min && flower.PetalWidth < max)
                                    {
                                        table[1][i] = table[1][i] + 1;
                                    }
                                    break;
                                case "Iris-virginica":
                                    if (flower.PetalWidth >= min && flower.PetalWidth < max)
                                    {
                                        table[2][i] = table[2][i] + 1;
                                    }
                                    break;
                            }
                        }
                    }
                    break;
            }
            List<double> verticalresult = new List<double>();
            for (int i = 0; i < table[0].Count; i++)
            {
                double sum = table[0][i] + table[1][i] + table[2][i];
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
            return table;
        }
    }
}
