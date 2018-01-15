using System.Drawing;

namespace Platform.Support.Drawing.Geometrics
{
    public static partial class Utilities
    {
        public static void DrawInsetCircle(ref Graphics g, ref System.Drawing.Rectangle r, Pen p)
        {
            int i;
            Pen p1 = new Pen(p.Color);
            Pen p2 = new Pen(p.Color);

            for (i = 0; i <= p.Width; i++)
            {
                System.Drawing.Rectangle r1 = new System.Drawing.Rectangle(r.X + i, r.Y + i, r.Width - i * 2, r.Height - i * 2);

                g.DrawArc(p2, r1, -45, 180);
                g.DrawArc(p1, r1, 135, 180);
            }
        }
    }
}