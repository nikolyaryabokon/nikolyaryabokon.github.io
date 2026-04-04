// File: Figures/Figure.cs
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace Task_7_R
{
    /// <summary>
    /// Базовый класс для всех фигур
    /// </summary>
    [Serializable]
    public abstract class Figure
    {
        protected Rectangle _bounds;
        protected bool _selected;
        protected Color _fillColor = Color.Transparent;

        /// <summary>
        /// Границы фигуры (прямоугольная область)
        /// </summary>
        public Rectangle Bounds
        {
            get => _bounds;
            set => _bounds = value;
        }

        /// <summary>
        /// Цвет заливки фигуры
        /// </summary>
        public Color FillColor
        {
            get => _fillColor;
            set => _fillColor = value;
        }

        /// <summary>
        /// Выделена ли фигура
        /// </summary>
        public bool Selected
        {
            get => _selected;
            set => _selected = value;
        }

        protected Figure(Rectangle bounds)
        {
            _bounds = bounds;
            _selected = false;
        }

        /// <summary>
        /// Отрисовка фигуры
        /// </summary>
        public abstract void Draw(Graphics g, Pen pen);

        /// <summary>
        /// Проверка, находится ли точка внутри фигуры
        /// </summary>
        public abstract bool HitTest(Point point);

        /// <summary>
        /// Сдвиг фигуры на dx, dy
        /// </summary>
        public virtual void Move(int dx, int dy)
        {
            _bounds.Offset(dx, dy);
        }

        /// <summary>
        /// Перемещение фигуры в указанную точку
        /// </summary>
        public virtual void MoveTo(Point newLocation)
        {
            _bounds.Location = newLocation;
        }

        /// <summary>
        /// Сдвиг на 1 пиксель
        /// </summary>
        public void MoveByOne(int dx, int dy) => Move(dx, dy);

        /// <summary>
        /// Сдвиг на 5 пикселей
        /// </summary>
        public void MoveByFive(int dx, int dy) => Move(dx * 5, dy * 5);

        /// <summary>
        /// Создание копии фигуры
        /// </summary>
        public abstract Figure Clone();
    }
}