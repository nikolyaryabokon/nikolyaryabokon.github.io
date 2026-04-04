// File: SelectionMarker.cs
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Task_7_R
{
    /// <summary>
    /// Класс для отображения маркеров выделения фигуры
    /// </summary>
    public class SelectionMarker
    {
        private const int MarkerSize = 8;
        private Rectangle _bounds;

        public SelectionMarker(Rectangle bounds)
        {
            _bounds = bounds;
        }

        public void UpdateBounds(Rectangle bounds)
        {
            _bounds = bounds;
        }

        public void Draw(Graphics g)
        {
            if (_bounds.IsEmpty) return;

            // Рисуем пунктирную рамку
            using (Pen dashPen = new Pen(Color.Black, 1))
            {
                dashPen.DashStyle = DashStyle.Dash;
                g.DrawRectangle(dashPen, _bounds);
            }

            // Рисуем маркеры по углам
            DrawMarker(g, _bounds.X - MarkerSize / 2, _bounds.Y - MarkerSize / 2);
            DrawMarker(g, _bounds.X + _bounds.Width - MarkerSize / 2, _bounds.Y - MarkerSize / 2);
            DrawMarker(g, _bounds.X - MarkerSize / 2, _bounds.Y + _bounds.Height - MarkerSize / 2);
            DrawMarker(g, _bounds.X + _bounds.Width - MarkerSize / 2, _bounds.Y + _bounds.Height - MarkerSize / 2);

            // Маркеры по серединам сторон
            DrawMarker(g, _bounds.X + _bounds.Width / 2 - MarkerSize / 2, _bounds.Y - MarkerSize / 2);
            DrawMarker(g, _bounds.X + _bounds.Width / 2 - MarkerSize / 2, _bounds.Y + _bounds.Height - MarkerSize / 2);
            DrawMarker(g, _bounds.X - MarkerSize / 2, _bounds.Y + _bounds.Height / 2 - MarkerSize / 2);
            DrawMarker(g, _bounds.X + _bounds.Width - MarkerSize / 2, _bounds.Y + _bounds.Height / 2 - MarkerSize / 2);
        }

        private void DrawMarker(Graphics g, int x, int y)
        {
            using (Brush fillBrush = new SolidBrush(Color.White))
            using (Pen borderPen = new Pen(Color.Black, 1))
            {
                g.FillRectangle(fillBrush, x, y, MarkerSize, MarkerSize);
                g.DrawRectangle(borderPen, x, y, MarkerSize, MarkerSize);
            }
        }

        /// <summary>
        /// Проверка попадания в маркер
        /// </summary>
        public HitMarkerType HitTest(Point point)
        {
            // Проверка угловых маркеров
            if (IsPointInMarker(point, _bounds.X - MarkerSize / 2, _bounds.Y - MarkerSize / 2))
                return HitMarkerType.TopLeft;
            if (IsPointInMarker(point, _bounds.X + _bounds.Width - MarkerSize / 2, _bounds.Y - MarkerSize / 2))
                return HitMarkerType.TopRight;
            if (IsPointInMarker(point, _bounds.X - MarkerSize / 2, _bounds.Y + _bounds.Height - MarkerSize / 2))
                return HitMarkerType.BottomLeft;
            if (IsPointInMarker(point, _bounds.X + _bounds.Width - MarkerSize / 2, _bounds.Y + _bounds.Height - MarkerSize / 2))
                return HitMarkerType.BottomRight;

            // Проверка маркеров по серединам
            if (IsPointInMarker(point, _bounds.X + _bounds.Width / 2 - MarkerSize / 2, _bounds.Y - MarkerSize / 2))
                return HitMarkerType.Top;
            if (IsPointInMarker(point, _bounds.X + _bounds.Width / 2 - MarkerSize / 2, _bounds.Y + _bounds.Height - MarkerSize / 2))
                return HitMarkerType.Bottom;
            if (IsPointInMarker(point, _bounds.X - MarkerSize / 2, _bounds.Y + _bounds.Height / 2 - MarkerSize / 2))
                return HitMarkerType.Left;
            if (IsPointInMarker(point, _bounds.X + _bounds.Width - MarkerSize / 2, _bounds.Y + _bounds.Height / 2 - MarkerSize / 2))
                return HitMarkerType.Right;

            return HitMarkerType.None;
        }

        private bool IsPointInMarker(Point point, int x, int y)
        {
            return point.X >= x && point.X <= x + MarkerSize &&
                   point.Y >= y && point.Y <= y + MarkerSize;
        }
    }

    public enum HitMarkerType
    {
        None,
        TopLeft,
        Top,
        TopRight,
        Left,
        Right,
        BottomLeft,
        Bottom,
        BottomRight
    }
}