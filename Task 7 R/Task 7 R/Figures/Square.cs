// File: Figures/Square.cs
using System;
using System.Drawing;

namespace Task_7_R
{
    /// <summary>
    /// Квадрат
    /// </summary>
    [Serializable]
    public class Square : Figure
    {
        public Square(Rectangle bounds) : base(bounds)
        {
            // Обеспечиваем квадратную форму
            MakeSquare();
        }

        private void MakeSquare()
        {
            int size = Math.Min(_bounds.Width, _bounds.Height);
            _bounds.Width = size;
            _bounds.Height = size;
        }

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
            return new Square(new Rectangle(_bounds.X, _bounds.Y, _bounds.Width, _bounds.Height))
            {
                FillColor = this.FillColor,
                Selected = this.Selected
            };
        }
    }
}