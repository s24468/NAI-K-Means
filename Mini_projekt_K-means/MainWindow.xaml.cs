using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Documents;

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
            var algorithm = new Algorithm(3, 1000);
            (points, var groupPoint) = algorithm.getGroups(points);

            var iterations = 0;
            bool hasChanged;
            do
            {
                hasChanged = Algorithm.AssignPoints(groupPoint, points);
                iterations++;
            } while (hasChanged && iterations < algorithm.MaxIterarion);

            DisplayResults(groupPoint);
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