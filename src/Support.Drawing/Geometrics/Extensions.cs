using System.Drawing;

namespace Platform.Support.Drawing.Geometrics
{
    public static partial class Extensions
    {
        public static bool IsPointInRectangle(this System.Drawing.Rectangle r, Point p)
        {
            bool flag = false;
            if ((p.X > r.X & p.X < r.X + r.Width & p.Y > r.Y & p.Y < r.Y + r.Height))
                flag = true;

            return flag;
        }

        public static void DrawInsetCircle(this Graphics @this, ref System.Drawing.Rectangle r, Pen p)
        {
            Utilities.DrawInsetCircle(ref @this, ref r, p);
        }
    }
}