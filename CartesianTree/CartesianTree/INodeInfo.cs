namespace CartesianTree
{
    /// <summary>
    /// Интерфейс, реализующий хранение информации в дереве и ее обновление
    /// </summary>
    /// <typeparam name="TValue">Информация, которая хранится в узле дерева</typeparam>
    interface INodeInfo<TValue>
    {
        /// <summary>
        /// Значение узла
        /// </summary>
        TValue Value { get; set; }

        /// <summary>
        /// Обновление информации в узле, в соответствии с ее левым и правом сыном
        /// </summary>
        /// <param name="left">Левый сын</param>
        /// <param name="right">Правый сын</param>
        void Update(INodeInfo<TValue> left, INodeInfo<TValue> right);
    }
}
