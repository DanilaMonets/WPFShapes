using System.Collections.Generic;
using System.Windows;

namespace LogicLibrary
{
    /// <summary>
    /// Collection of points
    /// </summary>
    public static class MyPointCollection
    {
        public static List<Point> collection;
        static MyPointCollection()
        {
            collection = new List<Point>();
        }
        public static void addPoint(Point p)
        {
            collection.Add(p);
        }
    }
}
