using System.Collections.Generic;
using System.Windows.Shapes;

namespace LogicLibrary
{
    /// <summary>
    /// Collection of points
    /// </summary>
    public static class PointEllipseCollection
    {
        public static List<Ellipse> collection;

        /// <summary>
        /// Out constructor without any paramethers
        /// </summary>
        static PointEllipseCollection()
        {
            collection = new List<Ellipse>();
        }
    }
}
