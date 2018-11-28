using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;
using LogicLibrary;
using HexagonLibrary;

namespace WPFShapes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Logic logic;
        public bool isDone;
        public bool shapeIsChosen;

        public MainWindow()
        {
            InitializeComponent();
            logic = new Logic();
            isDone = false;
            shapeIsChosen = false;
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, MenuItem_Click));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, MenuItem_Click_1));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, MenuItem_Click_2));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs, MenuItem_Click_3));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, closeClick));
            ShapesListMenu.ItemsSource = logic.hexagonCollection;
            ContextMenuItems.ItemsSource = logic.hexagonCollection;

            logic.hexagonCollection.CollectionChanged += Shapes_CollectionChanged;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            logic.hexagonCollection.Clear();
            shapeCanvas.Children.Clear();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            isDone = false;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            // Process save file dialog box results
            if (result == true)
            {
                logic.hexagonCollection.Clear();
                shapeCanvas.Children.Clear();
                Logic.counter = 1;

                ObservableCollection<HexagonShape> polygons = new ObservableCollection<HexagonShape>();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<HexagonShape>), new Type[] { typeof(HexagonShape) });

                using (FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    polygons = (ObservableCollection<HexagonShape>)xmlSerializer.Deserialize(fs);
                }

                foreach (var item in polygons)
                {
                    //logic.hexagonCollection.Add(item);
                    foreach (var elem in item.PointList)
                    {
                        MyPointCollection.addPoint(elem);
                    }
                    Action act = drawHexagon;
                    logic.createAndDrawHexagon(act, item.Color);
                }

                foreach (var item in polygons)
                {
                    //shapeCanvas.Children.Add(logic.createNewHexagon());
                }
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            //SaveFileDialog saveFileDialog = new SaveFileDialog
            //{
            dlg.FileName = "Untitled";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Xml documents (.xml)|*.xml";
            //};
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                logic.saveShapes(dlg.FileName);
            }
        }

        /// <summary>
        /// Color dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorClick(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) // ... == DialogResult.OK)
            {
                System.Drawing.Color color = colorDialog.Color;
                System.Windows.Media.Color newColor = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
                //  something.whatToColor = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B)
                logic.setColor(newColor);
            }
        }

        /// <summary>
        /// Draws hexagon
        /// </summary>
        public void drawHexagon()
        {
            foreach (var item in PointEllipseCollection.collection)
                shapeCanvas.Children.Remove(item);
            shapeCanvas.Children.Add(logic.createNewHexagon());
            PointEllipseCollection.collection.RemoveAll(a => a is Ellipse);
        }

        /// <summary>
        /// Choose polygon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hexagon_click(object sender, RoutedEventArgs e)
        {
            isDone = true;
            logic.UnchooseShape();
        }

        /// <summary>
        /// Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Point drawing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Mouse</param>
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isDone && e.LeftButton == MouseButtonState.Pressed)
            {
                MyPointCollection.addPoint(Mouse.GetPosition(shapeCanvas));
                Ellipse el = new Ellipse
                {
                    Fill = System.Windows.Media.Brushes.Black,
                    Height = 2,
                    Width = 2,
                    Margin = new Thickness(Mouse.GetPosition(shapeCanvas).X, Mouse.GetPosition(shapeCanvas).Y, 0, 0)
                };
                PointEllipseCollection.collection.Add(el);
                shapeCanvas.Children.Add(el);
                Action act = drawHexagon;
                logic.createAndDrawHexagon(act);
            }
        }

        /// <summary>
        /// Canvas context menu click choose shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Shapes_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.MenuItem menuItem = sender as System.Windows.Controls.MenuItem;

            logic.ChooseShape(menuItem.Header.ToString());
            shapeCanvas.Children[logic.ChosenIndex].MouseDown += CanvasChildren_MouseDown;
            isDone = false;
        }

        private void CanvasChildren_MouseDown(object sender, MouseButtonEventArgs e)
        {
            logic.SetShapeMarginAndStartMovePoint(Mouse.GetPosition(shapeCanvas));
            shapeCanvas.MouseMove += CanvasContainer_MouseMove;
        }

        private void CanvasContainer_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                logic.MoveShape(Mouse.GetPosition(shapeCanvas));
            }

            if (e.LeftButton == MouseButtonState.Released)
            {
                shapeCanvas.MouseMove -= CanvasContainer_MouseMove;
            }
        }

        /// <summary>
        /// Changed property
        /// </summary>
        /// <param name="sender"> Polygon shape</param>
        /// <param name="e"></param>
        private void Shapes_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChoosen")
            {
                int strokeThickness = 1;
                if ((sender as HexagonShape).IsChoosen)
                {
                    strokeThickness = 2;
                }

                (shapeCanvas.Children[logic.ChosenIndex] as Shape).StrokeThickness = strokeThickness;
            }
            else if (e.PropertyName == "Margin")
            {
                (shapeCanvas.Children[logic.ChosenIndex] as Shape).Margin = new Thickness((sender as HexagonShape).Margin.X, (sender as HexagonShape).Margin.Y, 0, 0);
            }
            else if (e.PropertyName == "Color")
            {
                (shapeCanvas.Children[logic.ChosenIndex] as Shape).Stroke = new SolidColorBrush((sender as HexagonShape).Color);
                (shapeCanvas.Children[logic.ChosenIndex] as Shape).Fill = new SolidColorBrush((sender as HexagonShape).Color);
            }
        }

        /// <summary>
        /// Changes shapes collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shapes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                (e.NewItems[0] as INotifyPropertyChanged).PropertyChanged += Shapes_PropertyChanged;
        }
    }
}
