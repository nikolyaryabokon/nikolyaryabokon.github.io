// File: Figures/IsoscelesTrapezoid.cs
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Task_7_R
{
    /// <summary>
    /// Равнобедренная трапеция
    /// </summary>
    [Serializable]
    public class IsoscelesTrapezoid : Figure
    {
        public IsoscelesTrapezoid(Rectangle bounds) : base(bounds) { }

        public override void Draw(Graphics g, Pen pen)
        {
            Point[] points = GetPoints();

            if (_fillColor != Color.Transparent)
            {
                using (Brush brush = new SolidBrush(_fillColor))
                {
                    g.FillPolygon(brush, points);
                }
            }
            g.DrawPolygon(pen, points);
        }

        private Point[] GetPoints()
        {
            int topWidth = _bounds.Width / 2;
            int topOffset = (_bounds.Width - topWidth) / 2;

            Point p1 = new Point(_bounds.X + topOffset, _bounds.Y);
            Point p2 = new Point(_bounds.X + topOffset + topWidth, _bounds.Y);
            Point p3 = new Point(_bounds.X + _bounds.Width, _bounds.Y + _bounds.Height);
            Point p4 = new Point(_bounds.X, _bounds.Y + _bounds.Height);

            return new Point[] { p1, p2, p3, p4 };
        }

        public override bool HitTest(Point point)
        {
            if (!_bounds.Contains(point))
                return false;

            // Простая проверка: попадание в ограничивающий прямоугольник
            return true;
        }

        public override Figure Clone()
        {
            return new IsoscelesTrapezoid(new Rectangle(_bounds.X, _bounds.Y, _bounds.Width, _bounds.Height))
            {
                FillColor = this.FillColor,
                Selected = this.Selected
            };
        }
    }
}