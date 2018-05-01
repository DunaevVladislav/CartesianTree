using System;

namespace CartesianTree
{
    /// <summary>
    /// Класс, реализующий хранение информации в дереве и ее обновление
    /// </summary>
    /// <typeparam name="TValue">Информация, которая хранится в узле дерева</typeparam>
    class NodeInfo<TValue> : IComparable where TValue : IComparable
    {
        /// <summary>
        /// Значение узла
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="value">Пользоватльская информация в текущем дереве</param>
        public NodeInfo(TValue value)
        {
            Value = value;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public NodeInfo() { }

        /// <summary>
        /// Обновление информации в узле, в соответствии с ее левым и правом сыном
        /// </summary>
        /// <param name="left">Левый сын</param>
        /// <param name="right">Правый сын</param>
        public virtual void Update(NodeInfo<TValue> left, NodeInfo<TValue> right) { }

        /// <summary>
        /// Компаратор
        /// </summary>
        /// <param name="obj">Объект, с которым производится сравение</param>
        /// <returns>Результат сравнения</returns>
        public int CompareTo(object obj)
        {
            if (obj is NodeInfo<TValue>)
            {
                return Value.CompareTo((obj as NodeInfo<TValue>).Value);
            }
            else
            {
                throw new Exception("Compare with the non-admissible class");
            }
        }

        /// <summary>
        /// Оператор больше
        /// </summary>
        /// <param name="operand1">Первый операнд</param>
        /// <param name="operand2">Второй операнд</param>
        /// <returns>Результат сравнения</returns>
        public static bool operator >(NodeInfo<TValue> operand1, NodeInfo<TValue> operand2)
        {
            return operand1.CompareTo(operand2) == 1;
        }

        /// <summary>
        /// Оператор меньше
        /// </summary>
        /// <param name="operand1">Первый операнд</param>
        /// <param name="operand2">Второй операнд</param>
        /// <returns>Результат сравнения</returns>
        public static bool operator <(NodeInfo<TValue> operand1, NodeInfo<TValue> operand2)
        {
            return operand1.CompareTo(operand2) == -1;
        }

        /// <summary>
        /// Оператор больше или равно
        /// </summary>
        /// <param name="operand1">Первый операнд</param>
        /// <param name="operand2">Второй операнд</param>
        /// <returns>Результат сравнения</returns>
        public static bool operator >=(NodeInfo<TValue> operand1, NodeInfo<TValue> operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        /// <summary>
        /// Оператор меньше или равно
        /// </summary>
        /// <param name="operand1">Первый операнд</param>
        /// <param name="operand2">Второй операнд</param>
        /// <returns>Результат сравнения</returns>
        public static bool operator <=(NodeInfo<TValue> operand1, NodeInfo<TValue> operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }

        /// <summary>
        /// Возвращает строку, представляющую текущий объект 
        /// </summary>
        /// <returns>Строку, представляющую текущий объект </returns>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
