using System;
using System.Collections.Generic;

namespace Mini_projekt_K_means
{
    public class Algorithm
    {
        private int KGroup { get; }
        public int MaxIterarion { get; }

        public Algorithm(int kGroup, int maxIterarion)
        {
            KGroup = kGroup;
            MaxIterarion = maxIterarion;
        }

        public (List<Point>, List<List<Point>>) GetGroups(List<Point> list)//tylko centroidy
        {
            var dataPoint = list;
            var groupPoint = new List<List<Point>>();

            for (var i = 0; i < KGroup; i++)
            {
                var random = new Random();
                var randomNumber = random.Next(0, list.Count - 1);

                groupPoint.Add(new List<Point>());
                groupPoint[i].Add(dataPoint[randomNumber]);
                dataPoint.RemoveAt(randomNumber);
            }

            return (dataPoint, groupPoint);
        }

        public static (bool, double) AssignPoints(List<List<Point>> groupPoint, List<Point> dataPoint)
        {
            var changed = false;

            var newGroupPoint = new List<List<Point>>();
            double sum = 0;
            //aby było tyle grup ile potrzeba 
            foreach (var group in groupPoint)
            {
                newGroupPoint.Add(new List<Point>());
            }

            //dopisywanie/kopiowanie centroidów
            double blankValue = 0;
            for (int i = 0; i < groupPoint.Count; i++)
            {
                (int closestCentroidIndex, blankValue) =
                    GiveClosestCentroidIndexAndMinDistance(groupPoint, groupPoint[i][0]);

                newGroupPoint[closestCentroidIndex].Add(groupPoint[i][0]);
                if (!groupPoint[closestCentroidIndex].Contains(groupPoint[i][0]))
                {
                    changed = true;
                }
            }


            foreach (var point in dataPoint)
            {
                //do której grupy
                double distance = 0;
                (var closestCentroidIndex, distance) = GiveClosestCentroidIndexAndMinDistance(groupPoint, point);

                newGroupPoint[closestCentroidIndex].Add(point);
                sum += distance;
                
                //warunek "kiedy przydziały do grup pozostaną niezmienione w dwóch kolejnych iteracjach"
                //sprawdź czy był w grupie z tym centroidem
                // Jeśli punkt został przypisany do innej grupy, ustaw wartość `changed` na `true`.

                if (!groupPoint[closestCentroidIndex].Contains(point))
                {
                    changed = true;
                }
            }

            // Aktualizacja grup punktów
            for (var i = 0; i < groupPoint.Count; i++)
            {
                groupPoint[i] = newGroupPoint[i];
            }

            return (changed, sum);
        }
//do której grupu punkt 
        private static (int, double) GiveClosestCentroidIndexAndMinDistance(List<List<Point>> groupPoint, Point point)
        {
            var minDistance = double.MaxValue;
            var closestCentroidIndex = 0;
            for (var i = 0; i < groupPoint.Count; i++)
            {
                var centroid = GiveCentroid(groupPoint[i]);//z każdym nowym punktem razem jest nowy centroid
                var distance = Point.GiveDistanceBetweenPoints(centroid, point);

                if (distance < minDistance)
                {
                    closestCentroidIndex = i;
                    minDistance = distance;
                }
            }

            return (closestCentroidIndex, minDistance);
        }

        private static Point GiveCentroid(List<Point> list)
        {
            var average = new Point(0, 0, 0, 0, "średnia");
            foreach (var point in list)
            {
                for (var i = 0; i < 4; i++)
                {
                    average.Coordinate[i] += point.Coordinate[i];
                }
            }

            for (var i = 0; i < 4; i++)
            {
                average.Coordinate[i] /= list.Count;
            }

            return average;
        }
    }
}