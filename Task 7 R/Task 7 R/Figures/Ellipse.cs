    // File: Figures/Ellipse.cs
    using System;
    using System.Drawing;

    namespace Task_7_R
    {
        /// <summary>
        /// Эллипс
        /// </summary>
        [Serializable]
        public class EllipseFigure : Figure
        {
            public EllipseFigure(Rectangle bounds) : base(bounds) { }

            public override void Draw(Graphics g, Pen pen)
            {
                if (_fillColor != Color.Transparent)
                {
                    using (Brush brush = new SolidBrush(_fillColor))
                    {
                        g.FillEllipse(brush, _bounds);
                    }
                }
                g.DrawEllipse(pen, _bounds);
            }

            public override bool HitTest(Point point)
            {
                if (!_bounds.Contains(point))
                    return false;

                // Эллиптическая проверка попадания
                float centerX = _bounds.X + _bounds.Width / 2f;
                float centerY = _bounds.Y + _bounds.Height / 2f;
                float rx = _bounds.Width / 2f;
                float ry = _bounds.Height / 2f;

                float dx = (point.X - centerX) / rx;
                float dy = (point.Y - centerY) / ry;
                return dx * dx + dy * dy <= 1.0;
            }

            public override Figure Clone()
            {
                return new EllipseFigure(new Rectangle(_bounds.X, _bounds.Y, _bounds.Width, _bounds.Height))
                {
                    FillColor = this.FillColor,
                    Selected = this.Selected
                };
            }
        }
    }