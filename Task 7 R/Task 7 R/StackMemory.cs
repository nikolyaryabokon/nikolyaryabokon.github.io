// File: StackMemory.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Task_7_R
{
    /// <summary>
    /// Описание класса для хранения последних изменений данных
    /// </summary>
    [Serializable]
    public class StackMemory
    {
        private readonly int _stackDepth;
        private readonly List<byte[]> _list = new List<byte[]>();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="depth">глубина стека</param>
        public StackMemory(int depth)
        {
            _stackDepth = depth;
            if (depth < 1) _stackDepth = 1;
            _list.Clear();
        }

        /// <summary>
        /// Помещаем данные в стек
        /// </summary>
        public void Push(MemoryStream stream)
        {
            if (_list.Count >= _stackDepth)
                _list.RemoveAt(0);
            _list.Add(stream.ToArray());
        }

        /// <summary>
        /// Очищаем стек
        /// </summary>
        public void Clear()
        {
            _list.Clear();
        }

        /// <summary>
        /// Количество сохранённых версий в стеке
        /// </summary>
        public int Count => _list.Count;

        /// <summary>
        /// Извлечение данных из стека
        /// </summary>
        public void Pop(MemoryStream stream)
        {
            if (_list.Count <= 0) return;
            var buff = _list[_list.Count - 1];
            stream.Write(buff, 0, buff.Length);
            _list.RemoveAt(_list.Count - 1);
        }

        /// <summary>
        /// Получить последние данные без удаления
        /// </summary>
        public byte[] Peek()
        {
            if (_list.Count <= 0) return null;
            return _list[_list.Count - 1];
        }
    }
}