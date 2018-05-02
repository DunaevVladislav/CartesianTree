namespace CartesianTree
{
    /// <summary>
    /// Интерфейс, дает возможность получить болеe подробную информацию используя метод ToString()
    /// </summary>
    interface IDetailsToString
    {
        /// <summary>
        /// Информация о текущем объекте
        /// </summary>
        /// <param name="details">Выводить ли подробности</param>
        /// <returns>Информацию о текущем объекте</returns>
        string ToString(bool details = false);
    }
}
