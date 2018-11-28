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
        /// Initializes static members of the <see cref="PointEllipseCollection" /> class.
        /// </summary>
        static PointEllipseCollection()
        {
            collection = new List<Ellipse>();
        }
    }
}
