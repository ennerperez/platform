using System.Drawing;

namespace Platform.Support.Drawing
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
            int i;
            Pen p1 = new Pen(p.Color);
            Pen p2 = new Pen(p.Color);

            for (i = 0; i <= p.Width; i++)
            {
                System.Drawing.Rectangle r1 = new System.Drawing.Rectangle(r.X + i, r.Y + i, r.Width - i * 2, r.Height - i * 2);

                @this.DrawArc(p2, r1, -45, 180);
                @this.DrawArc(p1, r1, 135, 180);
            }
        }
    }
}