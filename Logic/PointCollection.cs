//  <copyright file="MyPointCollection" company="NIP">
//  Copyright © 2018. All rights reserved.
//  </copyright>
//  <author>Danylo Monets</author>
//  <date>09/15/2018 05:09:42 PM </date>
//  <summary>Class representing a Point Collection</summary>

namespace LogicLibrary
{
    using System.Collections.Generic;
    using System.Windows;

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
