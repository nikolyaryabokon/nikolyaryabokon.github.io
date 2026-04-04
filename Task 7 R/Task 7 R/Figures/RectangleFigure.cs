// File: Figures/RectangleFigure.cs
using System;
using System.Drawing;

namespace Task_7_R
{
    /// <summary>
    /// Прямоугольник
    /// </summary>
    [Serializable]
    public class RectangleFigure : Figure
    {
        public RectangleFigure(Rectangle bounds) : base(bounds) { }

        public override void Draw(Graphics g, Pen pen)
        {
            if (_fillColor != Color.Transparent)
            {
                using (Brush brush = new SolidBrush(_fillColor))
                {
                    g.FillRectangle(brush, _bounds);
                }
            }
            g.DrawRectangle(pen, _bounds);
        }

        public override bool HitTest(Point point)
        {
            return _bounds.Contains(point);
        }

        public override Figure Clone()
        {
            return new RectangleFigure(new Rectangle(_bounds.X, _bounds.Y, _bounds.Width, _bounds.Height))
            {
                FillColor = this.FillColor,
                Selected = this.Selected
            };
        }
    }
}