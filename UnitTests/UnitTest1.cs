using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLibrary;
using HexagonLibrary;
using Serialization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace UnitTests
{
    /// <summary>
    /// UnitTest class
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        HexagonShape Polygons;
        Logic logic;
        Serialization.Serialization serial;

        public UnitTest1()
        {

            serial = new Serialization.Serialization();
        }
        /// <summary>
        /// SetColor test
        /// </summary>
        [TestMethod]
        public void SetColorTest()
        {
            logic = new Logic();
            logic.setColor(Colors.Blue);
        }
        /// <summary>
        /// CreateNewPolygonWith3Points test
        /// </summary>
        [TestMethod]
        public void CreateNewHexagonWith3PointsTest()
        {

            logic = new Logic();
            logic.hexagon.PointList = new System.Collections.Generic.List<Point>();
            logic.hexagon.PointList.Add(new Point(5, -5));
            logic.hexagon.PointList.Add(new Point(10, -10));
            logic.hexagon.PointList.Add(new Point(20, -20));
            Polygon p = logic.CreateNewHexagon();

            Assert.AreEqual(p.Points[0].X, 5);
        }
        /// <summary>
        /// ChoosePolygon test
        /// </summary>
        [TestMethod]
        public void ChooseHexagonTest1()
        {

            HexagonShape shape = new HexagonShape("Hello", new System.Collections.Generic.List<Point>() { new Point(5, -5), new Point(10, -10), new Point(20, -20) });
            HexagonShape shape1 = new HexagonShape("Pryvit", new System.Collections.Generic.List<Point>() { new Point(5, -5), new Point(10, -10), new Point(20, -20) });
            logic = new Logic();
            logic.hexagonCollection.Add(shape);
            logic.hexagonCollection.Add(shape1);

            logic.ChooseShape("Hello");

            Assert.AreEqual(0, logic.ChosenIndex);
        }
        /// <summary>
        /// ChoosePolygon test
        /// </summary>
        [TestMethod]
        public void ChooseHexagonTest2()
        {
            HexagonShape shape = new HexagonShape("Hello", new System.Collections.Generic.List<Point>() { new Point(5, -5), new Point(10, -10), new Point(20, -20) });
            HexagonShape shape1 = new HexagonShape("Pryvit", new System.Collections.Generic.List<Point>() { new Point(5, -5), new Point(10, -10), new Point(20, -20) });
            logic = new Logic();
            logic.hexagonCollection.Add(shape);
            logic.hexagonCollection.Add(shape1);

            logic.ChooseShape("Hello");

            Assert.AreEqual(true, logic.hexagonCollection[logic.ChosenIndex].IsChoosen);
        }
        /// <summary>
        /// ChoosePolygon test
        /// </summary>
        [TestMethod]
        public void ChooseHexagonTest3()
        {
            HexagonShape shape = new HexagonShape("Hello", new System.Collections.Generic.List<Point>() { new Point(5, -5), new Point(10, -10), new Point(20, -20) });
            HexagonShape shape1 = new HexagonShape("Pryvit", new System.Collections.Generic.List<Point>() { new Point(5, -5), new Point(10, -10), new Point(20, -20) });
            logic = new Logic();
            logic.hexagonCollection.Add(shape);
            logic.hexagonCollection.Add(shape1);

            logic.ChooseShape("Pryvit");
            Assert.AreEqual(false, logic.hexagonCollection[0].IsChoosen);
        }
        /// <summary>
        /// UnChooseShape test
        /// </summary>
        [TestMethod]
        public void UnchooseShapeTest()
        {
            HexagonShape shape = new HexagonShape("Hello", new System.Collections.Generic.List<Point>() { new Point(5, -5), new Point(10, -10), new Point(20, -20) });
            HexagonShape shape1 = new HexagonShape("Pryvit", new System.Collections.Generic.List<Point>() { new Point(5, -5), new Point(10, -10), new Point(20, -20) });
            logic = new Logic();
            logic.hexagonCollection.Add(shape);
            logic.hexagonCollection.Add(shape1);

            logic.ChooseShape("Hello");
            logic.UnchooseShape();

            Assert.AreEqual(false, logic.hexagonCollection[logic.ChosenIndex].IsChoosen);
        }
        /// <summary>
        /// ClearChoose test
        /// </summary>
        [TestMethod]
        public void ClearChooseTest()
        {
            HexagonShape shape = new HexagonShape("Hello", new System.Collections.Generic.List<Point>() { new Point(5, -5), new Point(10, -10), new Point(20, -20) });
            HexagonShape shape1 = new HexagonShape("Pryvit", new System.Collections.Generic.List<Point>() { new Point(5, -5), new Point(10, -10), new Point(20, -20) });
            logic = new Logic();
            logic.hexagonCollection.Add(shape);
            logic.hexagonCollection.Add(shape1);

            logic.ChooseShape("Hello");
            logic.ChooseShape("Pryvit");
            logic.ClearChoose();

            Assert.AreEqual(false, logic.hexagonCollection[logic.ChosenIndex].IsChoosen || logic.hexagonCollection[0].IsChoosen);
        }
        /// <summary>
        /// GetPoints test
        /// </summary>
        [TestMethod]
        public void GetPointsTest()
        {
            logic = new Logic();
            MyPointCollection.addPoint(new Point(5, -5));
            MyPointCollection.addPoint(new Point(10, -5));
            MyPointCollection.addPoint(new Point(11, -52));
            MyPointCollection.addPoint(new Point(53, -25));
            MyPointCollection.addPoint(new Point(15, -33));
            System.Collections.Generic.List<Point> list = logic.getPoints();

            Assert.AreEqual(5, list.Count);
        }
        /// <summary>
        /// RemoveAllPoints test
        /// </summary>
        [TestMethod]
        public void RemoveAllPointsTest()
        {
            logic = new Logic();
            MyPointCollection.addPoint(new Point(5, -5));
            MyPointCollection.addPoint(new Point(10, -5));
            MyPointCollection.addPoint(new Point(11, -52));
            MyPointCollection.addPoint(new Point(53, -25));
            MyPointCollection.addPoint(new Point(15, -33));
            logic.removeAllPoint();
            System.Collections.Generic.List<Point> list = logic.getPoints();
            Assert.AreEqual(0, list.Count);
        }
        /// <summary>
        /// Serialize test
        /// </summary>
        [TestMethod]
        public void SerializeTest()
        {
            Polygons = new HexagonShape("Zdorov", new List<Point>() { new Point(10, 20), new Point(5, -5), new Point(101, 401) }, true);
            ObservableCollection<HexagonShape> list = new ObservableCollection<HexagonShape>();
            list.Add(Polygons);
            HexagonShape poly = new HexagonShape("Abaldet", new List<Point>() { new Point(10, 201), new Point(5, -25), new Point(101, 421) });

            list.Add(poly);
            serial.saveShapes(@"forserialization.txt", list);

            Assert.AreEqual(true, File.Exists(@"forserialization.txt"));
        }
        /// <summary>
        /// Deserialize test
        /// </summary>
        [TestMethod]
        public void DeserializeTest()
        {
            ObservableCollection<HexagonShape> poly = serial.openShapes(@"forserialization.txt");

            Assert.AreEqual(10, poly[0].PointList[0].X);
        }
        /// <summary>
        /// GenerateName test
        /// </summary>
        [TestMethod]
        public void TestGenerateName()
        {
            logic = new Logic();
            string name = logic.generateName("Hexagon");

            Assert.AreEqual("Hexagon1", name);
        }
        /// <summary>
        /// SetShapeMarginAndStartMovePoint test
        /// </summary>
        [TestMethod]
        public void SetShapeMarginAndStartMovePointTest()
        {
            logic = new Logic();
            HexagonShape shape = new HexagonShape("Hello", new System.Collections.Generic.List<Point>() { new Point(5, -5), new Point(10, -10), new Point(20, -20) });
            shape.Margin = new Point(1, 2);
            HexagonShape shape1 = new HexagonShape("Pryvit", new System.Collections.Generic.List<Point>() { new Point(5, -5), new Point(10, -10), new Point(20, -20) });
            logic.hexagonCollection.Add(shape);
            logic.hexagonCollection.Add(shape1);

            logic.ChooseShape("Hello");
            Point p = new Point(5, -5);
            logic.SetShapeMarginAndStartMovePoint(p);

            Assert.AreEqual(shape.Margin.X, logic.marginShape.X);
        }
        /// <summary>
        /// Move shape test
        /// </summary>
        [TestMethod]
        public void MoveShapeTest()
        {
            logic = new Logic();
            HexagonShape shape = new HexagonShape("Hello", new System.Collections.Generic.List<Point>() { new Point(5, -5), new Point(10, -10), new Point(20, -20) });
            shape.Margin = new Point(1, 2);
            HexagonShape shape1 = new HexagonShape("Pryvit", new System.Collections.Generic.List<Point>() { new Point(5, -5), new Point(10, -10), new Point(20, -20) });
            logic.hexagonCollection.Add(shape);
            logic.hexagonCollection.Add(shape1);

            logic.ChooseShape("Hello");
            Point p = new Point(5, -5);
            logic.SetShapeMarginAndStartMovePoint(p);

            Point mousePoint = new Point(300, 500);

            logic.MoveShape(mousePoint);
            Point newMargin = new Point(296, 0);

            Assert.AreEqual(newMargin.X, logic.hexagonCollection[logic.ChosenIndex].Margin.X);
        }
    }
}
