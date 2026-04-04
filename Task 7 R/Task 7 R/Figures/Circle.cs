// File: Figures/Circle.cs
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Task_7_R
{
    /// <summary>
    /// Круг
    /// </summary>
    [Serializable]
    public class Circle : Figure
    {
        public Circle(Rectangle bounds) : base(bounds) { }

        public override void Draw(Graphics g, Pen pen)
        {
            // Заливка
            if (_fillColor != Color.Transparent)
            {
                using (Brush brush = new SolidBrush(_fillColor))
                {
                    g.FillEllipse(brush, _bounds);
                }
            }
            // Контур
            g.DrawEllipse(pen, _bounds);
        }

        public override bool HitTest(Point point)
        {
            if (!_bounds.Contains(point))
                return false;

            // Проверка попадания в круг (радиус)
            float centerX = _bounds.X + _bounds.Width / 2f;
            float centerY = _bounds.Y + _bounds.Height / 2f;
            float radius = _bounds.Width / 2f;

            float dx = point.X - centerX;
            float dy = point.Y - centerY;
            return dx * dx + dy * dy <= radius * radius;
        }

        public override Figure Clone()
        {
            return new Circle(new Rectangle(_bounds.X, _bounds.Y, _bounds.Width, _bounds.Height))
            {
                FillColor = this.FillColor,
                Selected = this.Selected
            };
        }
    }
}