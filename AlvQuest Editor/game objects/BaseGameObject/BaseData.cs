namespace AlvQuest_Editor
{
    /// <summary>
    /// Общие поля всех игровых сущностей  
    /// </summary>
    public class BaseData
    {
        /// <summary>
        /// Название игрового объекта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание игрового объекта.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Название файла - иконки.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Вычисляет хэш код в зависимости от содержимых данных.
        /// </summary>
        /// <returns><see cref="int"/> Хэш код конкретного объекта.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Icon);
        }
    }
}
