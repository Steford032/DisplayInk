using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DisplayInk
{
    public class ArrayPoints
    {
        private int index = 0;
        private Point[] points;

        public ArrayPoints(int size)
        {
            if (size <= 0) size = 2;
            points = new Point[size];
        }

        public void SetPoint(int x, int y)
        {
            if (index >= points.Length) index = 0;
            points[index] = new Point(x, y);
            index++;
        }

        public void Reset()
        {
            index = 0;
        }

        public int Index()
        {
            return index;
        }
        public Point[] getPoints()
        {
            return points;
        }

    }
}
