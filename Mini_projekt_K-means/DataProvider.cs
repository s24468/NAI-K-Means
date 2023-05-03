using System;
using System.Collections.Generic;
using System.IO;

namespace Mini_projekt_K_means
{
    public class DataProvider
    {
        public readonly List<Point> _Points;

        public DataProvider(string filePath)
        {
            var sr = new StreamReader(filePath);
            string line;
            _Points = new List<Point>();
            while ((line = sr.ReadLine()) != null)
            {
                var parts = line.Split(',');

                _Points.Add(new Point(Convert.ToDouble(parts[0].Replace('.', ',')),
                    Convert.ToDouble(parts[1].Replace('.', ',')),
                    Convert.ToDouble(parts[2].Replace('.', ',')), Convert.ToDouble(parts[3].Replace('.', ',')),
                    parts[4]));
            }
        }
    }
}