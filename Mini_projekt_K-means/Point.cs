using System;
using System.Collections.Generic;

namespace Mini_projekt_K_means
{
    public class Point
    {
        public readonly List<double> Coordinate;
        public readonly string Name;

        public Point(double x, double y, double z, double t, string name)
        {

            Coordinate = new List<double>
            {
                x,
                y,
                z,
                t
            };
            Name = name;
        }

        public static double GiveDistanceBetweenPoints(Point point1, Point point2)
        {
            double result = 0;
            for (int i = 0; i < 4; i++)
            {
                result += Math.Pow(point1.Coordinate[i] - point2.Coordinate[i], 2);
            }

            return Math.Sqrt(result);
        }

    }
}