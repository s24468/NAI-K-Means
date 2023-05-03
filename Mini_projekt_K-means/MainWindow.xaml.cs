using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

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
            var algorithm = new Algorithm(4, 100);
            List<List<Point>> groupPoint = new List<List<Point>>();
            (points, groupPoint) = algorithm.getGroups(points);

            int iterations = 0;
            bool hasChanged;
            do
            {
                hasChanged = Algorithm.AssignPoints(groupPoint, points);
                iterations++;
            } while (hasChanged && iterations < algorithm.MaxIterarion);

            DisplayResults(groupPoint);
          
        }
        
        private void DisplayResults(List<List<Point>> resultList2)
        {
            // Załóżmy, że resultList to lista list punktów otrzymana po klasteryzacji
            List<List<Point>> resultList = resultList2;
        
            int clusterIndex = 1;
            foreach (var cluster in resultList)
            {
                textBox.AppendText($"Klaster {clusterIndex}:\n");
        
                foreach (var point in cluster)
                {
                    textBox.AppendText($"X: {point._coordinate[0]}, Y: {point._coordinate[1]}, Z: {point._coordinate[2]}, T: {point._coordinate[3]}\n");
                }
        
                textBox.AppendText("\n");
                clusterIndex++;
            }
        }
        

        private static void showListPoints(List<Point> points)
        {
            int i = 0;
            foreach (var point in points)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(point._coordinate[j] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}