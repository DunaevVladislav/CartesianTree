using System;

namespace CartesianTree
{
    /// <summary>
    /// Класс, реализующий хранение информации в дереве и ее обновление
    /// </summary>
    /// <typeparam name="TValue">Информация, которая хранится в узле дерева</typeparam>
    abstract class NodeInfo<TValue>:IComparable where TValue:IComparable
    {
        /// <summary>
        /// Значение узла
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// Обновление информации в узле, в соответствии с ее левым и правом сыном
        /// </summary>
        /// <param name="left">Левый сын</param>
        /// <param name="right">Правый сын</param>
        public abstract void Update(NodeInfo<TValue> left, NodeInfo<TValue> right);

        /// <summary>
        /// Компаратор
        /// </summary>
        /// <param name="obj">Объект, с которым производится сравение</param>
        /// <returns>Результат сравнения</returns>
        public int CompareTo(object obj)
        {
            if (obj is NodeInfo<TValue>)
            {
                return this.CompareTo((obj as NodeInfo<TValue>));
            }
            else
            {
                throw new Exception("Compare with the non-admissible class");
            }
        }
    }
}
