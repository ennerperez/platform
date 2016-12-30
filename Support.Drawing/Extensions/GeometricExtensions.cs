using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Platform.Support.Drawing
{
    public static partial class GeometricExtensions
    {

        public static bool IsPointInRectangle(this Rectangle @this, Point point)
        {
            return GeometricHelper.IsPointInRectangle(point, @this);
        }
        
    }
}
