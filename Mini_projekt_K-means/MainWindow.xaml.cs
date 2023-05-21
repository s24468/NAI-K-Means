using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Mini_projekt_K_means
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var dataProvider =
                new DataProvider(@"C:\Users\Jarek\RiderProjects\Mini_projekt_K-means\Mini_projekt_K-means\iris.data");
            var points = dataProvider._Points;
            var algorithm = new Algorithm(10, 100);
            (points, var groupPoint) = algorithm.GetGroups(points);
            var iterations = 0;
            List<double> sumList = new List<double>();
            bool hasChanged;
            double sum = 0;
            do
            {
                
                (hasChanged, sum) = Algorithm.AssignPoints(groupPoint, points);
                sumList.Add(sum);
                iterations++;
            } while (hasChanged && iterations < algorithm.MaxIterarion);//iterations
            DisplaySumsOfIterations(sumList);

            DisplayResults(groupPoint);
        }

        private void DisplaySumsOfIterations(List<double> sumList)
        {
            var paragraph = new Paragraph();
            textBox.Document.Blocks.Add(paragraph);
            for (int j = 0; j < sumList.Count; j++)
            {
                paragraph.Inlines.Add(new Run($"Iteration {(j+1)} : {sumList[j]}:\n") { Foreground = Brushes.Black });
            }
        }
        private void DisplayResults(List<List<Point>> list)
        {
            var resultList = list ?? throw new Exception(nameof(list) + " IS EMPTY");

            var clusterIndex = 1;

            var paragraph = new Paragraph();
            textBox.Document.Blocks.Add(paragraph);

            foreach (var cluster in resultList)
            {
                paragraph.Inlines.Add(new Run($"Klaster {clusterIndex}:\n") { Foreground = Brushes.Black });

                foreach (var point in cluster)
                {
                    paragraph.Inlines.Add(
                        new Run(
                                $"X: {point.Coordinate[0]}, Y: {point.Coordinate[1]}, Z: {point.Coordinate[2]}, T: {point.Coordinate[3]}, name: ")
                            { Foreground = Brushes.Black });

                    switch (point.Name)
                    {
                        case "Iris-setosa":
                            paragraph.Inlines.Add(new Run($"{point.Name}\n") { Foreground = Brushes.Red });
                            break;
                        case "Iris-versicolor":
                            paragraph.Inlines.Add(new Run($"{point.Name}\n") { Foreground = Brushes.Blue });
                            break;
                        default:
                            paragraph.Inlines.Add(new Run($"{point.Name}\n") { Foreground = Brushes.Green });
                            break;
                    }
                }

                paragraph.Inlines.Add(new Run($"\n"));

                clusterIndex++;
            }
        }
    }
}