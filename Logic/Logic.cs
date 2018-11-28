using HexagonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;

namespace LogicLibrary
{
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
        public List<Point> getPoints()
        {
            return new List<Point>(MyPointCollection.collection);
        }
        public void removeAllPoint()
        {
            MyPointCollection.collection.RemoveAll(a => a is Point);
        }

        public Polygon createNewHexagon()
        {
            Polygon h = new Polygon();
            foreach (var item in hexagon.PointList)
            {
                h.Points.Add(item);
            }
            h.Stroke = Brushes.Black;
            h.Fill = new SolidColorBrush(hexagon.Color);
            h.Margin = new Thickness(hexagon.Margin.X, hexagon.Margin.Y, 0, 0);
            return h;
        }

        /// <summary>
        /// Unchooses shape
        /// </summary>
        public void UnchooseShape()
        {
            if (ChosenIndex != -1)
                hexagonCollection[ChosenIndex].IsChoosen = false;

        }
        /// <summary>
        /// Chooses shape
        /// </summary>
        /// <param name="s">Shape name</param>
        public void ChooseShape(string s)
        {
            ClearChoose();
            ChosenIndex = hexagonCollection.IndexOf(hexagonCollection.Where(a => a.Name == s).First());
            hexagonCollection[ChosenIndex].IsChoosen = true;
        }
        /// <summary>
        /// Unchooses everything
        /// </summary>
        public void ClearChoose()
        {
            foreach (var item in hexagonCollection)
            {
                item.IsChoosen = false;
            }
        }
        /// <summary>
        /// Sets margin and start point
        /// </summary>
        /// <param name="startMovePoint"></param>
        public void SetShapeMarginAndStartMovePoint(Point startMovePoint)
        {
            marginShape = hexagonCollection[ChosenIndex].Margin;
            this.startMovePoint = startMovePoint;
        }
        /// <summary>
        /// Moves shape
        /// </summary>
        /// <param name="mousePoint">Mouse point</param>
        public void MoveShape(Point mousePoint)
        {
            Point newMarginPoint = new Point(mousePoint.X - startMovePoint.X + marginShape.X, mousePoint.Y - startMovePoint.Y + marginShape.Y);
            hexagonCollection[ChosenIndex].Margin = newMarginPoint;
        }
        /// <summary>
        /// Chooses color
        /// </summary>
        /// <param name="color">Color</param>   
        public void setColor(Color color)
        {
            if (color != Color.FromRgb(255, 255, 255))
            {
                if (ChosenIndex != -1)
                    hexagonCollection[ChosenIndex].Color = color;
            }
        }
        /// <summary>
        /// Saves object as xml document
        /// </summary>
        /// <param name="path">Color</param>   
        public void saveShapes(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(hexagonCollection.GetType(), new Type[] { typeof(HexagonShape) });
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                xmlSerializer.Serialize(fs, hexagonCollection);
            }
        }
    }
}
