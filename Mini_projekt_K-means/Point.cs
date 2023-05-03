using System;
using System.Collections.Generic;

namespace Mini_projekt_K_means
{
    public class Point
    {
        public readonly List<double> _coordinate;
        private string _name;

        public Point(double x, double y, double z, double t, string name)
        {

            _coordinate = new List<double>
            {
                x,
                y,
                z,
                t
            };
            _name = name;
        }

        public static double GiveDistanceBetweenPoints(Point point1, Point point2)
        {
            double result = 0;
            for (int i = 0; i < 4; i++)
            {
                result += Math.Pow(point1._coordinate[i] - point2._coordinate[i], 2);
            }

            return Math.Sqrt(result);
        }

    }
}