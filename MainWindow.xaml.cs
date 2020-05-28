using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using LiveCharts.Wpf.Components;

namespace CAIM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainClass Main = new MainClass();
        List<DataStorage> datas = new List<DataStorage>();
        public MainWindow()
        {
            InitializeComponent();
            datas = Main.AlgorithOrchestrator();
            SetChart();
        }

        void SetChart()
        {
            SepalSetosa = new ChartValues<ObservablePoint>();
            SepalVersicolor = new ChartValues<ObservablePoint>();
            SepalVirginica = new ChartValues<ObservablePoint>();
            PetalSetosa = new ChartValues<ObservablePoint>();
            PetalVersicolor = new ChartValues<ObservablePoint>();
            PetalVirginica = new ChartValues<ObservablePoint>();
            foreach (var flower in Main.Flowers)
            {
                switch (flower.ClassName)
                {
                    case ("Iris-setosa"):
                        SepalSetosa.Add(new ObservablePoint(flower.SepalLength, flower.SepalWidth));
                        PetalSetosa.Add(new ObservablePoint(flower.PetalLength, flower.PetalWidth));
                        break;
                    case ("Iris-versicolor"):
                        SepalVersicolor.Add(new ObservablePoint(flower.SepalLength, flower.SepalWidth));
                        PetalVersicolor.Add(new ObservablePoint(flower.PetalLength, flower.PetalWidth));
                        break;
                    case ("Iris-virginica"):
                        SepalVirginica.Add(new ObservablePoint(flower.SepalLength, flower.SepalWidth));
                        PetalVirginica.Add(new ObservablePoint(flower.PetalLength, flower.PetalWidth));
                        break;
                }
            }

            ChartValues<double> SepalLengthChartValues = new ChartValues<double>();
            ChartValues<double> SepalWidthChartValues = new ChartValues<double>();
            ChartValues<double> PetalLengthChartValues = new ChartValues<double>();
            ChartValues<double> PetalWidthChartValues = new ChartValues<double>();
            

            foreach (var data in datas)
            {
                switch (data.DataStored)
                {
                    case ("SepalLength"):
                        foreach (var interval in data.Interval)
                        {
                            SepalLengthChartValues = new ChartValues<double>();
                            for (int i = 0; i < 8; i++)
                            {
                                SepalLengthChartValues.Add(interval);
                            }
                            scSepal.Series.Add(new VerticalLineSeries()
                            {
                                Values = SepalLengthChartValues,
                                StrokeDashArray = new DoubleCollection { 2 },
                                IsHitTestVisible = false,
                                PointGeometry = null,
                                PointForeground = null,
                                Focusable = false,
                                DataLabels = false,
                            });
                        }
                        break;
                    case ("SepalWidth"):
                        foreach (var interval in data.Interval)
                        {
                            SepalWidthChartValues = new ChartValues<double>();
                            for (int i = 0; i < 8; i++)
                            {
                                SepalWidthChartValues.Add(interval);
                            }
                            scSepal.Series.Add(new LineSeries()
                            {
                                Values = SepalWidthChartValues,
                                StrokeDashArray = new DoubleCollection { 2 },
                                IsHitTestVisible = false,
                                PointGeometry = null,
                                PointForeground = null,
                                Focusable = false,
                                DataLabels = false,
                            });
                        }
                        break;
                    case ("PetalLength"):
                        foreach (var interval in data.Interval)
                        {
                            PetalLengthChartValues = new ChartValues<double>();
                            for (int i = 3; i < 8; i++)
                            {
                                PetalLengthChartValues.Add(interval);
                            }
                            scPetal.Series.Add(new VerticalLineSeries()
                            {
                                Values = PetalLengthChartValues,
                                StrokeDashArray = new DoubleCollection { 2 },
                                IsHitTestVisible = false,
                                PointGeometry = null,
                                PointForeground = null,
                                Focusable = false,
                                DataLabels = false,

                            });
                        }
                        break;
                    case ("PetalWidth"):
                        foreach (var interval in data.Interval)
                        {
                            PetalWidthChartValues = new ChartValues<double>();
                            for (int i = 0; i < 8; i++)
                            {
                                PetalWidthChartValues.Add(interval);
                            }
                            scPetal.Series.Add(new LineSeries()
                            {
                                Values = PetalWidthChartValues,
                                StrokeDashArray = new DoubleCollection { 2 },
                                IsHitTestVisible = false,
                                PointGeometry = null,
                                PointForeground = null,
                                Focusable = false,
                                DataLabels = false,
                            });
                        }
                        break;
                }
            }
            scSepal.DataContext = this;
            scPetal.DataContext = this;
        }

        public ChartValues<ObservablePoint> SepalSetosa { get; set; }
        public ChartValues<ObservablePoint> SepalVersicolor { get; set; }
        public ChartValues<ObservablePoint> SepalVirginica { get; set; }
        public ChartValues<ObservablePoint> PetalSetosa { get; set; }
        public ChartValues<ObservablePoint> PetalVersicolor { get; set; }
        public ChartValues<ObservablePoint> PetalVirginica { get; set; }

        public SeriesCollection SeriesPetal { get; set; }
    }
}
