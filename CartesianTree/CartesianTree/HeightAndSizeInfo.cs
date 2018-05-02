using System;

namespace CartesianTree
{
    /// <summary>
    /// Информация для хранения в декартовом дереве
    /// Хранит число, размер и высоту поддерева
    /// </summary>
    class HeightAndSizeInfo:IComparable, IDetailsToString
    {
        /// <summary>
        /// Значение
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Размер поддерева
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Высота поддерева
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="value">Значение узла</param>
        public HeightAndSizeInfo(int value = 0)
        {
            Value = value;
            Size = 1;
            Height = 1;
        }

        /// <summary>
        /// Компаратор
        /// </summary>
        /// <param name="obj">Обхъект, с которым происходит сравнение</param>
        /// <returns>Результат сравнения</returns>
        public int CompareTo(object obj)
        {
            if (obj is HeightAndSizeInfo)
            {
                return Value.CompareTo((obj as HeightAndSizeInfo).Value);
            }
            return Value.CompareTo(obj);
        }

        /// <summary>
        /// Возвращает строку, представляющую текущий объект 
        /// </summary>
        /// <param name="details">Нужна ли подробная информация</param>
        /// <returns>Строку, представляющую текущий объект </returns>
        public string ToString(bool details = false)
        {
            if (details)
            {
                return  "Число: " + Value.ToString() + "\n" +
                     "Размер поддерева: " + Size.ToString() + "\n" +
                     "Высота поддерева: " + Height.ToString() + "\n";
            }
            return Value.ToString();
        }
    }
}
