//  <copyright file="Logic.cs" company="NIP">
//  Copyright © 2018. All rights reserved.
//  </copyright>
//  <author>Danylo Monets</author>
//  <date>09/15/2018 05:09:42 PM </date>
//  <summary>Class representing a Logic of our program</summary>

namespace LogicLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Shapes;
    using System.Windows.Media;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;
    using System.IO;
    using HexagonLibrary;

    /// <summary>
    /// Class contains methods to create and manipulate a shape, logic
    /// </summary>
    public class Logic
    {
        public Point marginShape;
        public Point startMovePoint;

        public HexagonShape hexagon { get; set; }

        public ObservableCollection<HexagonShape> hexagonCollection
        {
            get;
            set;
        }

        public static int counter;
        public int ChosenIndex { get; set; } = -1;

        static Logic()
        {
            counter = 1;
        }

        public Logic()
        {
            hexagon = new HexagonShape();
            hexagonCollection= new ObservableCollection<HexagonShape>();
        }

        /// <summary>
        /// Creates a hexagon
        /// </summary>
        /// <param name="act"></param>
        public void createAndDrawHexagon(Action act, Color? color = null)
        {
            List<Point> points = getPoints();
            if (points.Count == 6)
            {
                hexagon = new HexagonShape(generateName("Hexagon"), points);
                if (color != null)
                {
                    hexagon.Color = (Color)color;
                }
                hexagonCollection.Add(hexagon);
                act();
                removeAllPoint();
            }
        }
        /// <summary>
        /// Names the shape
        /// </summary>
        /// <param name="s">Name</param>
        /// <returns>Name + count</returns>
        public string generateName(string s)
        {
            return s + counter++.ToString();
        }

        /// <summary>
        /// Get points
        /// </summary>
        /// <returns>List of points</returns>
        public List<Point> getPoints()
        {
            return new List<Point>(MyPointCollection.collection);
        }

        /// <summary>
        /// Remove all points
        /// </summary>
        public void removeAllPoint()
        {
            MyPointCollection.collection.RemoveAll(a => a is Point);
        }

        /// <summary>
        /// createNewHexagon() method
        /// </summary>
        /// <returns>returns Polygon</returns>
        public Polygon CreateNewHexagon()
        {
            Polygon h = new Polygon();
            foreach (var item in this.hexagon.PointList)
            {
                h.Points.Add(item);
            }

            h.Stroke = Brushes.Black;
            h.Fill = new SolidColorBrush(this.hexagon.Color);
            h.Margin = new Thickness(this.hexagon.Margin.X, this.hexagon.Margin.Y, 0, 0);
            return h;
        }

        /// <summary>
        /// Unchoose shape
        /// </summary>
        public void UnchooseShape()
        {
            if (this.ChosenIndex != -1)
            {
                this.hexagonCollection[this.ChosenIndex].IsChoosen = false;
            }
        }
        
        /// <summary>
        /// Chooses shape
        /// </summary>
        /// <param name="s">Shape name</param>
        public void ChooseShape(string s)
        {
            this.ClearChoose();
            this.ChosenIndex = this.hexagonCollection.IndexOf(this.hexagonCollection.Where(a => a.Name == s).First());
            this.hexagonCollection[this.ChosenIndex].IsChoosen = true;
        }

        /// <summary>
        /// Unchoose everything
        /// </summary>
        public void ClearChoose()
        {
            foreach (var item in this.hexagonCollection)
            {
                item.IsChoosen = false;
            }
        }

        /// <summary>
        /// Sets margin and start point
        /// </summary>
        /// <param name="startMovePoint">First variable</param>
        public void SetShapeMarginAndStartMovePoint(Point startMovePoint)
        {
            this.marginShape = this.hexagonCollection[this.ChosenIndex].Margin;
            this.startMovePoint = startMovePoint;
        }

        /// <summary>
        /// Moves shape
        /// </summary>
        /// <param name="mousePoint">Mouse point</param>
        public void MoveShape(Point mousePoint)
        {
            Point newMarginPoint = new Point(mousePoint.X - this.startMovePoint.X + this.marginShape.X, mousePoint.Y - this.startMovePoint.Y + this.marginShape.Y);
            this.hexagonCollection[this.ChosenIndex].Margin = newMarginPoint;
        }

        /// <summary>
        /// Chooses color
        /// </summary>
        /// <param name="color">Color of hexagon</param>   
        public void setColor(Color color)
        {
            if (color != Color.FromRgb(255, 255, 255))
            {
                if (this.ChosenIndex != -1)
                {
                    this.hexagonCollection[this.ChosenIndex].Color = color;
                }
            }
        }

        /// <summary>
        /// Saves object as xml document
        /// </summary>
        /// <param name="path">Path to file</param>   
        public void saveShapes(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.hexagonCollection.GetType(), new Type[] { typeof(HexagonShape) });
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                xmlSerializer.Serialize(fs, this.hexagonCollection);
            }
        }
    }
}
