// File: Stroke.cs
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Task_7_R
{
    /// <summary>
    /// Описание класса хранения свойств для рисования контура фигуры
    /// </summary>
    [Serializable]
    public class Stroke
    {
        private const int DefaultAlpha = 255;

        public Stroke()
        {
            Color = Color.Black;
            Width = 2f;
            DashStyle = DashStyle.Solid;
        }

        /// <summary>
        /// Цвет линии фигуры
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Ширина линии фигуры
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Стиль линии фигуры
        /// </summary>
        public DashStyle DashStyle { get; set; }

        /// <summary>
        /// Прозрачность
        /// </summary>
        public int Alpha { get; set; } = DefaultAlpha;

        /// <summary>
        /// Свойство возвращает "карандаш", настроенный по текущим свойствам
        /// </summary>
        public Pen UpdatePen(Pen pen)
        {
            if (pen == null)
                throw new ArgumentNullException(nameof(pen));

            pen.Color = Color.FromArgb(Alpha, Color);
            pen.Width = Width;
            pen.DashStyle = DashStyle;
            return pen;
        }

        /// <summary>
        /// Создание нового пера
        /// </summary>
        public Pen CreatePen()
        {
            return new Pen(Color.FromArgb(Alpha, Color), Width)
            {
                DashStyle = DashStyle
            };
        }
    }
}