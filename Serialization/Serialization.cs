namespace Serialization
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Xml.Serialization;
    using HexagonLibrary;

    /// <summary>
    /// Xml serialization class
    /// </summary>
    public class Serialization
    {
        /// <summary>
        /// Save shape
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <param name="polygonShapes">Shape</param>
        public void saveShapes(string path, ObservableCollection<HexagonShape> polygonShapes)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(polygonShapes.GetType(), new Type[] { typeof(HexagonShape) });
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                xmlSerializer.Serialize(fs, polygonShapes);
            }
        }

        /// <summary>
        /// Open shape
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>Shape</returns>
        public ObservableCollection<HexagonShape> openShapes(string path)
        {
            ObservableCollection<HexagonShape> hexagons = new ObservableCollection<HexagonShape>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<HexagonShape>), new Type[] { typeof(HexagonShape) });

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                hexagons = (ObservableCollection<HexagonShape>)xmlSerializer.Deserialize(fs);
            }

            return hexagons;
        }
    }
}
