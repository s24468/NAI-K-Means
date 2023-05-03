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

        public (List<Point>, List<List<Point>>) getGroups(List<Point> list)
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

        public static bool AssignPoints(List<List<Point>> groupPoint, List<Point> dataPoint)
        {
            bool changed = false;

            var newGroupPoint = new List<List<Point>>();

            foreach (var group in groupPoint)
            {
                newGroupPoint.Add(new List<Point>());
            }

            for (int i = 0; i < groupPoint.Count; i++)
            {
                int closestCentroidIndex = GiveClosestCentroidIndex(groupPoint, groupPoint[i][0]);

                newGroupPoint[closestCentroidIndex].Add(groupPoint[i][0]);
                if (!groupPoint[closestCentroidIndex].Contains(groupPoint[i][0]))
                {
                    changed = true;
                }
            }

            foreach (var point in dataPoint)
            {
                var closestCentroidIndex = GiveClosestCentroidIndex(groupPoint, point);

                newGroupPoint[closestCentroidIndex].Add(point);

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

            return changed;
        }

        private static int GiveClosestCentroidIndex(List<List<Point>> groupPoint, Point point)
        {
            var minDistance = double.MaxValue;
            var closestCentroidIndex = 0;
            for (var i = 0; i < groupPoint.Count; i++)
            {
                var centroid = giveCentroid(groupPoint[i]);
                var distance = Point.GiveDistanceBetweenPoints(centroid, point);

                if (distance < minDistance)
                {
                    closestCentroidIndex = i;
                    minDistance = distance;
                }
            }

            return closestCentroidIndex;
        }

        private static Point giveCentroid(List<Point> list)
        {
            var average = new Point(0, 0, 0, 0, "średnia");
            foreach (var point in list)
            {
                for (var i = 0; i < 4; i++)
                {
                    average._coordinate[i] += point._coordinate[i];
                }
            }

            for (var i = 0; i < 4; i++)
            {
                average._coordinate[i] /= list.Count;
            }

            return average;
        }
    }
}